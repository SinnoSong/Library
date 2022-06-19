using System;

namespace Library.API.Models
{
    public class LendConfigVo
    {
        public Guid Id { get; set; }
        public byte ReaderGrade { get; set; }
        public int MaxLendNumber { get; set; }
        public int MaxLendDays { get; set; }
    }
}
