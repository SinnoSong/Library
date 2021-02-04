using Newtonsoft.Json;

namespace Library.API.Models
{
    public class Link
    {
        public Link(string method, string rel, string href)
        {
            Method = method;
            Relation = rel;
            Href = href;
        }

        public string Method { get; }

        [JsonProperty("rel")]
        public string Relation { get; }

        public string Href { get; }
    }
}