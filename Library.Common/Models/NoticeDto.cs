using Newtonsoft.Json;

namespace Library.Common.Models
{
    public class NoticeDto
    {
        [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }

        [JsonProperty("title", Required = Required.Always)]
        public string Title { get; set; }

        [JsonProperty("content", Required = Required.Always)]
        public string Content { get; set; }

        [JsonProperty("createTime", Required = Required.Always)]
        public DateTime CreateTime { get; set; }

        public NoticeDto(Guid id, string title, string content, DateTime createTime)
        {
            Id = id;
            Title = title;
            Content = content;
            CreateTime = createTime;
        }
    }
}