using System;

namespace Library.Common.Models
{
    public class LendConfigVo
    {
        public Guid Id { get; set; }
        public byte ReaderGrade { get; set; }
        public int MaxLendNumber { get; set; }
        public int MaxLendDays { get; set; }
    }
}
