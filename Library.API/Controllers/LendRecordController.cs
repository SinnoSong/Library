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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LendRecordController : ControllerBase
    {
        #region field
        private readonly ILendRecordService _lendRecordService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly HashFactory _hashFactory;
        private readonly Dictionary<string, PropertyMapping> mappingDict;
        #endregion


        #region ctor

        public LendRecordController(IBookService bookService, ILendRecordService lendRecordService, IMapper mapper, HashFactory hashFactory)
        {
            _lendRecordService = lendRecordService;
            _bookService = bookService;
            _mapper = mapper;
            _hashFactory = hashFactory;
            mappingDict = new Dictionary<string, PropertyMapping>
            {
                {"id",new PropertyMapping("Id")},
                {"studentId",new PropertyMapping("StudentId") },
                {"bookId",new PropertyMapping("BookId") },
                {"startTime",new PropertyMapping("StartTime") },
                {"endTime",new PropertyMapping("EndTime") },
                {"returnTime",new PropertyMapping("RealReturnTime") },
            };
        }
        #endregion

        #region Get

        [HttpGet(Name = nameof(GetLendRecordsAsync))]
        public async Task<ActionResult<PagedList<LendRecordVo>>> GetLendRecordsAsync(string sort, ulong? studentId = null, string? lendTime = null, string? returnTime = null, int page = 1, int pageSize = 25)
        {
            var records = await _lendRecordService.GetAllAsync();
            Expression<Func<LendRecord, bool>>? select = default;
            if (studentId != null)
            {
                select = record => record.StudentId == studentId;
            }
            if (lendTime != null)
            {
                if (select == null)
                {
                    select = record => record.StartTime.Date == DateTime.Parse(lendTime);
                }
                else
                {
                    select = select.And(record => record.StartTime.Date == DateTime.Parse(lendTime));
                }
            }
            if (returnTime != null)
            {
                if (select == null)
                {
                    select = record => record.StartTime.Date == DateTime.Parse(returnTime);
                }
                else
                {
                    select = select.And(record => record.StartTime.Date == DateTime.Parse(returnTime));
                }
            }
            if (select != null)
            {
                records = records.Where(select);
            }
            records = records.Sort(sort, mappingDict);
            return await PagedList<LendRecordVo>.CreateAsync(_mapper.ProjectTo<LendRecordVo>(records), page, pageSize);
        }

        [HttpGet("{id}", Name = nameof(GetRecordAsync))]
        public async Task<ActionResult<LendRecordVo>> GetRecordAsync(Guid id)
        {
            var record = await _lendRecordService.GetByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            var entityNewHash = _hashFactory.GetHash(record);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return _mapper.Map<LendRecordVo>(record);
        }
        #endregion

        #region Post

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public async Task<IActionResult> AddRecordAsync(LendRecordForCreationDto dto)
        {
            var book = await _bookService.GetByIdAsync(dto.BookId);
            if (book == null)
            {
                throw new Exception($"Id: {dto.BookId}的书籍不存在！");
            }
            if (book.IsLend)
            {
                throw new Exception("该书籍已经被租借！");
            }
            var result = await _lendRecordService.AddAsync(_mapper.Map<LendRecord>(dto));
            if (result == null)
            {
                throw new Exception("租借失败，请稍后再试！");
            }
            var vo = _mapper.Map<LendRecordVo>(result);
            return CreatedAtAction(nameof(GetRecordAsync), new { id = vo.Id }, vo);
        }
        #endregion

        #region Put

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        [CheckIfMatchHeaderFilter]
        public async Task<IActionResult> PutAsync(Guid id, LendRecordForCreationDto dto)
        {
            var record = await _lendRecordService.GetByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            var entityHash = _hashFactory.GetHash(record);
            if (Request.Headers.TryGetValue(HeaderNames.IfMatch, out var requestETag) && requestETag != entityHash)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }
            _mapper.Map(dto, record);
            await _lendRecordService.UpdateAsync(record);
            var entityNewHash = _hashFactory.GetHash(record);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return NoContent();
        }
        #endregion
    }
}
