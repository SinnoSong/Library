using Library.API.Configs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Library.API.Helper
{
    public class PagedListConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(PagedList<T>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObj = JObject.Load(reader);
            var totalCount = (int)jsonObj["totalCount"];
            var pageNumber = (int)jsonObj["pageNumber"];
            var pageSize = (int)jsonObj["pageSize"];
            var items = jsonObj["Items"].ToObject<T[]>(serializer);
            PagedList<T> pagedList = new PagedList<T>(items.ToList(), totalCount, pageNumber, pageSize);
            return pagedList;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            PagedList<T> result = (PagedList<T>)value;
            JObject jsonObj = new JObject
            {
                { "totalCount", result.TotalCount },
                { "pageNumber", result.CurrentPage },
                { "pageSize", result.PageSize },
                { "Items", JArray.FromObject(result.ToArray(), serializer) }
            };
            jsonObj.WriteTo(writer);
        }
    }
}