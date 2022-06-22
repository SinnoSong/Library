using Newtonsoft.Json;

namespace Library.Common.Models
{
    public class LendRecordCreateDto
    {
        [JsonProperty("userId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid UserId { get; set; }

        [JsonProperty("bookId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid BookId { get; set; }
    }
}