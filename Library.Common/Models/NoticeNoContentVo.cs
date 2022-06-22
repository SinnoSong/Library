
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Common.Models
{
    public class NoticeNoContentVo
    {
        public Guid Id { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }

        public NoticeNoContentVo(Guid id, string title, DateTime createTime)
        {
            Id = id;
            Title = title;
            CreateTime = createTime;
        }
    }
}
