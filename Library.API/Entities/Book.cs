using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string? Summary { get; set; } = null;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }
        [MaxLength(50)]
        [Column("ISBN")]
        public string Isbn { get; set; }

        [MaxLength(250)]
        public string? Image { get; set; }
        public int Pages { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        public Guid AuthorId { get; set; }
    }
}