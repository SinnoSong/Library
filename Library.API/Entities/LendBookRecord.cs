using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.Entities
{
    public class LendBookRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public ulong StudentId { get; set; }
        [Required]
        public Guid BookId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime RealReturnTime { get; set; }
        public string Processer { get; set; }

        #region ctor

        public LendBookRecord(string processer)
        {
            Processer = processer;
        }

        #endregion
    }
}
