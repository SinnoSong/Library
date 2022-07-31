using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.Entities;

public class Book
{
    #region ctor

    public Book(string title, string author, string location)
    {
        Title = title;
        Author = author;
        Location = location;
    }

    #endregion

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] [MaxLength(100)] public string Title { get; set; }

    [MaxLength(250)] public string? Summary { get; set; }

    [Column(TypeName = "decimal(18, 2)")] public decimal? Price { get; set; }

    [MaxLength(50)] [Column("ISBN")] public string? Isbn { get; set; }

    [MaxLength(250)] public string? Image { get; set; }

    public int Pages { get; set; }

    [MaxLength(100)] public string Author { get; set; }

    [MaxLength(250)] public string Location { get; set; }

    public bool IsLend { get; set; }
    public Guid CategoryId { get; set; }
}