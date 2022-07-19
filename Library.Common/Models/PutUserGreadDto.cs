using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Common.Models
{
    public class PutUserGreadDto
    {
        [Required, MinLength(4)]
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("changeGrade")] public byte Grade { get; set; }
    }
}

