
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class NoticeVo
    {
        public Guid Id { get; set; }
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(1000)]
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }

        public NoticeVo(Guid id, string title, string content, DateTime createTime)
        {
            Id = id;
            Title = title;
            Content = content;
            CreateTime = createTime;
        }
    }
}
