using System;

namespace Library.API.Models
{
    public class LendRecordForCreationDto
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
