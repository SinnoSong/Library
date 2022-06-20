using System;

namespace Library.API.Models
{
    public class LendRecordVo
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime RealReturnTime { get; set; }
        public string Processer { get; set; }
        public LendRecordVo(string processer)
        {
            Processer = processer;
        }
    }
}
