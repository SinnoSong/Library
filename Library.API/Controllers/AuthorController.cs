using AutoMapper;
using Library.API.Models;
using Library.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Library.API.Entities;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            this._authorRepository = repositoryWrapper.Author;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthorsAsync()
        {
            var authors = (await _authorRepository.GetAllAsync()).OrderBy(a => a.Name);
            var authorDtoList = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return authorDtoList.ToList();
        }

        [HttpGet("{authorId}", Name = nameof(GetAuthorAsync))]
        public async Task<ActionResult<AuthorDto>> GetAuthorAsync(Guid authorId)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }
            return _mapper.Map<AuthorDto>(author);
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
            return CreatedAtRoute(nameof(GetAuthorAsync), new { authorID = authorCreated.Id }, authorCreated);
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
    }
}