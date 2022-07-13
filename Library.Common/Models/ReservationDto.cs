using Newtonsoft.Json;

namespace Library.Common.Models;

public class ReservationDto : LendRecordCreateDto
{
    [JsonProperty("startTime", Required = Required.Always)]
    public DateTime StartTime { get; set; }

    [JsonProperty("endTime", Required = Required.Always)]
    public DateTime EndTime { get; set; }
}