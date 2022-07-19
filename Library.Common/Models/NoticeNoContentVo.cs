using Newtonsoft.Json;

namespace Library.Common.Models;

public class NoticeNoContentVo
{
    public NoticeNoContentVo(Guid id, string title, DateTime createTime)
    {
        Id = id;
        Title = title;
        CreateTime = createTime;
    }

    [JsonProperty("id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public Guid Id { get; set; }

    [JsonProperty("title", Required = Required.Always)]
    public string Title { get; set; }

    [JsonProperty("createTime", Required = Required.Always)]
    public DateTime CreateTime { get; set; }
}