using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.API.Configs;
using Library.API.Configs.Filters;
using Library.API.Entities;
using Library.API.Extentions;
using Library.API.Helper;
using Library.API.Models;
using Library.API.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator,SuperAdministrator")]
    public class CategoryController : ControllerBase
    {

        #region field
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly HashFactory _hashFactory;
        private readonly Dictionary<string, PropertyMapping> mappingDict;
        #endregion

        #region ctor
        public CategoryController(IServicesWrapper repositoryWrapper, IMapper mapper, HashFactory hashFactory)
        {
            _categoryService = repositoryWrapper.Category;
            _mapper = mapper;
            _hashFactory = hashFactory;
            mappingDict = new Dictionary<string, PropertyMapping>()
            {
                {"id",new PropertyMapping("Id") },
                {"name",new PropertyMapping("Name") },
            };
        }
        #endregion

        #region Get

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<PagedList<CategoryVo>>> Get(string sort = "id", string? search = null, int page = 1, int pageSize = 25)
        {
            IQueryable<Category>? categories = default;
            if (search == null)
            {
                categories = await _categoryService.GetAllAsync();
            }
            else
            {
                categories = await _categoryService.GetByConditionAsync(category => category.Name.Contains(search));
            }
            categories = categories.Sort(sort, mappingDict);
            return await PagedList<CategoryVo>.CreateAsync(categories.ProjectTo<CategoryVo>(_mapper.ConfigurationProvider), page, pageSize);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}", Name = nameof(Get))]
        public async Task<ActionResult<CategoryVo>> Get(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var entityNewHash = _hashFactory.GetHash(category);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return _mapper.Map<CategoryVo>(category);
        }
        #endregion

        #region Post

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post(CategoryForCreationDto dto)
        {
            var result = await _categoryService.AddAsync(_mapper.Map<Category>(dto));
            if (result == null)
            {
                throw new Exception("创建资源Category失败！");
            }
            var vo = _mapper.Map<CategoryVo>(result);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, vo);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> AddCategoriesAsync(CategoryForCreationCollectionDto dto)
        {
            var categories = _mapper.Map<IEnumerable<Category>>(dto.Categories);
            await _categoryService.AddAsync(categories);
            if (!await _categoryService.SaveAsync())
            {
                throw new Exception("创建资源Category失败！");
            }
            return Ok(dto.Categories);
        }
        #endregion

        #region Put

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        [CheckIfMatchHeaderFilter]
        public async Task<IActionResult> PutAsync(Guid id, CategoryForCreationDto dto)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var entityHash = _hashFactory.GetHash(category);
            if (Request.Headers.TryGetValue(HeaderNames.IfMatch, out var requestETag) && requestETag != entityHash)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }
            _mapper.Map(dto, category);
            await _categoryService.UpdateAsync(category);
            var entityNewHash = _hashFactory.GetHash(category);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return NoContent();
        }
        #endregion

        #region Delete

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryService.DeleteAsync(category);
            return NoContent();
        }
        #endregion

    }
}
