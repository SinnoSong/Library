using Newtonsoft.Json;

namespace Library.Common.Models;

public class RegisterUser : RegisterAdmin
{
    [JsonProperty("grade")] public byte Grade { get; set; }
}