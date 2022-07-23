using Newtonsoft.Json;

namespace Library.Common.Models;

public class MenuPath
{
    public MenuPath(string path, string name, string? key = null, List<MenuPath>? children = null)
    {
        Key = key;
        Path = path;
        Children = children;
        Name = name;
    }

    public string? Key { get; set; }

    public string Path { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public List<MenuPath>? Children { get; set; }

    public string Name { get; set; }
}