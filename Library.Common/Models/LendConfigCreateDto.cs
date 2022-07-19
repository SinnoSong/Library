using Newtonsoft.Json;

namespace Library.Common.Models;

public class LendConfigCreateDto
{
    [JsonProperty("readerGrade", Required = Required.Always)]
    public byte ReaderGrade { get; set; }

    [JsonProperty("maxLendNumber", Required = Required.Always)]
    public int MaxLendNumber { get; set; }

    [JsonProperty("maxLendDays", Required = Required.Always)]
    public int MaxLendDays { get; set; }
}