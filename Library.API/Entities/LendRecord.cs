using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.Entities
{
    public class LendRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid BookId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime? RealReturnTime { get; set; }
        public Guid Processer { get; set; }

        #region ctor

        public LendRecord(Guid processer)
        {
            Processer = processer;
        }

        public LendRecord()
        {
        }

        #endregion
    }
}
