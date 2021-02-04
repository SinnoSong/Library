using Newtonsoft.Json;
using System.Collections.Generic;

namespace Library.API.Models
{
    public abstract class Resource
    {
        [JsonProperty("_link", Order = 100)]
        public List<Link> Links { get; } = new List<Link>();
    }
}