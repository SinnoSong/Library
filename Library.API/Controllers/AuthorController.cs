using AutoMapper;
using Library.API.Configs;
using Library.API.Entities;
using Library.API.Helper;
using Library.API.Models;
using Library.API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    [ApiController, Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorController> _logger;
        private readonly IDistributedCache _distributedCache;
        private readonly HashFactory _hashFactory;
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILogger<AuthorController> logger,
            IDistributedCache distributedCache, HashFactory hashFactory)
        {
            this._authorRepository = repositoryWrapper.Author;
            this._mapper = mapper;
            this._logger = logger;
            this._distributedCache = distributedCache;
            this._hashFactory = hashFactory;
        }

        [HttpGet(Name = (nameof(GetAuthorsAsync)))]
        public async Task<ActionResult<ResourceCollection<AuthorDto>>> GetAuthorsAsync([FromQuery] AuthorResourceParameters parameters)
        {
            PagedList<Author> pagedList = default;
            if (string.IsNullOrWhiteSpace(parameters.BirthPlace) && string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                string cacheKey = $"authors_page_{parameters.PageNumber}_pageSize_{parameters.PageSize}_{parameters.SortBy}"; //生成缓存键
                string cachedContent = await _distributedCache.GetStringAsync(cacheKey);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Converters.Add(new PagedListConverter<Author>());
                settings.Formatting = Formatting.Indented;
                if (string.IsNullOrWhiteSpace(cachedContent))
                {
                    pagedList = await _authorRepository.GetAllAsync(parameters);
                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    };
                    var serizlizedContent = JsonConvert.SerializeObject(pagedList, settings);
                    await _distributedCache.SetStringAsync(cacheKey, serizlizedContent);
                }
                else
                {
                    pagedList = JsonConvert.DeserializeObject<PagedList<Author>>(cachedContent, settings);
                }
            }
            else
            {
                pagedList = await _authorRepository.GetAllAsync(parameters);
            }
            var paginationMetaData = new
            {
                totalCount = pagedList.TotalCount,
                pageSize = pagedList.PageSize,
                currentPage = pagedList.CurrentPage,
                totalPages = pagedList.TotalPages,
                previousePageLink = pagedList.HasPrevious ? Url.Link(nameof(GetAuthorsAsync), new
                {
                    pageNumber = pagedList.CurrentPage - 1,
                    pageSize = pagedList.PageSize,
                    birthPlace = parameters.BirthPlace,
                    searchQuery = parameters.SearchQuery,
                    sortBy = parameters.SortBy
                }) : null,
                nextPageLink = pagedList.HasNext ? Url.Link(nameof(GetAuthorsAsync), new
                {
                    pageNumber = pagedList.CurrentPage + 1,
                    pageSize = pagedList.PageSize,
                    birthPlace = parameters.BirthPlace,
                    searchQuery = parameters.SearchQuery,
                    sortBy = parameters.SortBy
                }) : null
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetaData));
            var authorDtoList = _mapper.Map<IEnumerable<AuthorDto>>(pagedList);
            authorDtoList = authorDtoList.Select(author => CreateLinksForAuthor(author));
            var resourceList = new ResourceCollection<AuthorDto>(authorDtoList.ToList());
            return CreateLinksForAuthors(resourceList, parameters, paginationMetaData);
        }

        [HttpGet("{authorId}", Name = nameof(GetAuthorAsync))]
        //[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<ActionResult<AuthorDto>> GetAuthorAsync(Guid authorId)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }
            var entityHash = _hashFactory.GetHash(author);
            Response.Headers[HeaderNames.ETag] = entityHash;
            if (Request.Headers.TryGetValue(HeaderNames.IfNoneMatch, out var requestETag) && entityHash == requestETag)
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }
            var authorDto = _mapper.Map<AuthorDto>(author);
            return CreateLinksForAuthor(authorDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthorAsync(AuthorForCreationDto authorForCreationDto)
        {
            var author = _mapper.Map<Author>(authorForCreationDto);
            _authorRepository.Create(author);
            var result = await _authorRepository.SaveAsync();
            if (!result)
            {
                throw new Exception("创建资源author失败");
            }
            var authorCreated = _mapper.Map<AuthorDto>(author);
            return CreatedAtRoute(nameof(GetAuthorAsync), new { authorID = authorCreated.Id }, CreateLinksForAuthor(authorCreated));
        }

        [HttpDelete("{authorId}")]
        public async Task<IActionResult> DeleteAuthorAsync(Guid authorId)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }
            _authorRepository.Delete(author);
            var result = await _authorRepository.SaveAsync();
            if (!result)
            {
                throw new Exception("删除资源author失败");
            }
            return NoContent();
        }

        private AuthorDto CreateLinksForAuthor(AuthorDto author)
        {
            author.Links.Clear();
            author.Links.Add(new Link(HttpMethods.Get,
                "self",
                Url.Link(nameof(GetAuthorAsync), new { authorId = author.Id })
                ));
            author.Links.Add(new Link(HttpMethods.Delete,
                "delete author",
                Url.Link(nameof(DeleteAuthorAsync), new { authorId = author.Id })
                ));
            author.Links.Add(new Link(HttpMethods.Get,
                "author's books",
                Url.Link(nameof(BookController.GetBooksAsync), new { authorId = author.Id })
                ));
            return author;
        }

        private ResourceCollection<AuthorDto> CreateLinksForAuthors(ResourceCollection<AuthorDto> authors, AuthorResourceParameters parameters = null, dynamic paginationData = null)
        {
            authors.Links.Clear();
            authors.Links.Add(new Link(HttpMethods.Get,
                "self",
                Url.Link(nameof(GetAuthorsAsync), parameters)
                ));
            authors.Links.Add(new Link(HttpMethods.Post,
                "create author",
                Url.Link(nameof(CreateAuthorAsync), null)
                ));
            if (paginationData != null)
            {
                if (paginationData.previousePageLink != null)
                {
                    authors.Links.Add(new Link(HttpMethods.Get,
                        "previous page",
                        paginationData.previousePageLink
                        ));
                }
                if (paginationData.nextPageLink != null)
                {
                    authors.Links.Add(new Link(HttpMethods.Get,
                        "next page",
                        paginationData.nextPageLink
                        ));
                }
            }
            return authors;
        }
    }
}