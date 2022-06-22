using System;

namespace Library.Common.Models
{
    public class LendRecordForCreationDto
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
