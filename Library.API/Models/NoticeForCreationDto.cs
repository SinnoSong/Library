using System;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class NoticeForCreationDto
    {
        [Required, MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(1000)]
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public NoticeForCreationDto(string title, string content, DateTime createTime)
        {
            Title = title;
            Content = content;
            CreateTime = createTime;
        }
    }
}
