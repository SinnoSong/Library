using Newtonsoft.Json;

namespace Library.Common.Models;

public class UserDto : UserBase
{
    [JsonProperty("grade")] public byte? Grade { get; set; }
}