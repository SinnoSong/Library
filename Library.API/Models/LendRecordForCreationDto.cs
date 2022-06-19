using System;

namespace Library.API.Models
{
    public class LendRecordForCreationDto
    {
        public ulong StudentId { get; set; }
        public Guid BookId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime RealReturnTime { get; set; }
        public string Processer { get; set; }

        public LendRecordForCreationDto(string processer)
        {
            Processer = processer;
        }
    }
}
