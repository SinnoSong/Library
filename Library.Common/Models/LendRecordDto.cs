using Newtonsoft.Json;

namespace Library.Common.Models
{
    public class LendRecordDto
    {
        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }

        [JsonProperty("bookId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid BookId { get; set; }

        [JsonProperty("startTime", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime StartTime { get; set; }

        [JsonProperty("endTime", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime EndTime { get; set; }

        [JsonProperty("returnTime", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime RealReturnTime { get; set; }

        [JsonProperty("processor", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Processor { get; set; }

        public LendRecordDto(string processor)
        {
            Processor = processor;
        }
    }
}