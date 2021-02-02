using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public abstract class Resource
    {
        [JsonProperty("_link", Order = 100)]
        public List<Link> Links { get; } = new List<Link>();
    }
}