using AutoMapper;
using Library.API.Entities;
using Library.API.Helper;
using Library.Common.Models;
using Library.API.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoticeController : ControllerBase
    {
        #region field
        private readonly INoticeService _noticeService;
        private readonly IMapper _mapper;
        private readonly HashFactory _hashFactory;
        #endregion

        #region ctor
        public NoticeController(IServicesWrapper repositoryWrapper, IMapper mapper, HashFactory hashFactory)
        {
            _noticeService = repositoryWrapper.Notice;
            _mapper = mapper;
            _hashFactory = hashFactory;
        }
        #endregion

        #region Get

        [HttpGet]
        public async Task<ActionResult<PagedList<NoticeNoContentVo>>> GetNoticesAsync(int page = 1, int pageSize = 25)
        {
            var notices = await _noticeService.GetAllAsync();
            notices = notices.OrderByDescending(notice => notice.CreateTime);
            return await PagedList<NoticeNoContentVo>.CreateAsync(_mapper.ProjectTo<NoticeNoContentVo>(notices), page, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoticeVo>> GetNoticeAsync(Guid id)
        {
            var notice = await _noticeService.GetByIdAsync(id);
            if (notice == null)
            {
                return NotFound();
            }
            var entityNewHash = _hashFactory.GetHash(notice);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return _mapper.Map<NoticeVo>(notice);
        }
        #endregion

        #region Post
        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public async Task<IActionResult> AddNotice(NoticeForCreationDto dto)
        {
            var notice = _mapper.Map<Notice>(dto);
            notice.CreateTime = DateTime.Now;
            var result = await _noticeService.AddAsync(notice);
            if (result == null)
            {
                throw new Exception("创建资源Notice失败！");
            }
            var vo = _mapper.Map<NoticeVo>(result);
            return CreatedAtAction(nameof(GetNoticeAsync), new { id = vo.Id }, vo);
        }
        #endregion

        #region Delete

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var notice = await _noticeService.GetByIdAsync(id);
            if (notice == null)
            {
                return NotFound();
            }
            await _noticeService.DeleteAsync(notice);
            return NoContent();
        }
        #endregion

        #region Put

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public async Task<IActionResult> PutAsync(Guid id, NoticeForCreationDto dto)
        {
            var notice = await _noticeService.GetByIdAsync(id);
            if (notice == null)
            {
                return NotFound();
            }
            var entityHash = _hashFactory.GetHash(notice);
            if (Request.Headers.TryGetValue(HeaderNames.IfMatch, out var requestETag) && requestETag != entityHash)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }
            _mapper.Map(dto, notice);
            await _noticeService.UpdateAsync(notice);
            var entityNewHash = _hashFactory.GetHash(notice);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return NoContent();
        }
        #endregion
    }
}
