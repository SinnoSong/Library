namespace Library.Common.Models
{
    public class LendConfigForCreationDto
    {
        public byte ReaderGrade { get; set; }
        public int MaxLendNumber { get; set; }
        public int MaxLendDays { get; set; }
    }
}
