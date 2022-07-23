using Newtonsoft.Json;

namespace Library.Common.Models;

public class UserDto : UserBase
{
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("grade")] public byte? Grade { get; set; }
}