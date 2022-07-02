using Newtonsoft.Json;

namespace Library.Common.Models;

public class LendRecordUpdateDto
{
    [JsonProperty("returnTime")] public DateTime ReturnTime { get; set; }
}