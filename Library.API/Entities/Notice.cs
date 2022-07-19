using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.Entities;

public class Notice
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] [MaxLength(50)] public string Title { get; set; }

    [Required] [MaxLength(1000)] public string Content { get; set; }

    public DateTime CreateTime { get; set; }

    #region ctor

    public Notice(string title, string content, DateTime createTime)
    {
        Title = title;
        Content = content;
        CreateTime = createTime;
    }

    public Notice()
    {
    }

    #endregion
}