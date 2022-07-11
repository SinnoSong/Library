using AutoMapper;
using Library.API.Entities;
using Library.API.Service.Interface;
using Library.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        #endregion

        #region ctor
        public NoticeController(IServicesWrapper repositoryWrapper, IMapper mapper)
        {
            _noticeService = repositoryWrapper.Notice;
            _mapper = mapper;
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
        public async Task<ActionResult<NoticeDto>> GetNoticeAsync(Guid id)
        {
            var notice = await _noticeService.GetByIdAsync(id);
            if (notice == null)
            {
                return NotFound();
            }
            return _mapper.Map<NoticeDto>(notice);
        }
        #endregion

        #region Post
        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public async Task<IActionResult> AddNotice(NoticeCreateDto dto)
        {
            var notice = _mapper.Map<Notice>(dto);
            notice.CreateTime = DateTime.Now;
            var result = await _noticeService.AddAsync(notice);
            if (result == null)
            {
                throw new Exception("创建资源Notice失败！");
            }
            var vo = _mapper.Map<NoticeDto>(result);
            return CreatedAtRoute(nameof(GetNoticeAsync), new { id = vo.Id }, vo);
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
        public async Task<IActionResult> PutAsync(Guid id, NoticeCreateDto dto)
        {
            var notice = await _noticeService.GetByIdAsync(id);
            if (notice == null)
            {
                return NotFound();
            }
            _mapper.Map(dto, notice);
            await _noticeService.UpdateAsync(notice);
            return NoContent();
        }
        #endregion
    }
}
