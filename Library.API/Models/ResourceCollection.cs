using System.Collections.Generic;

namespace Library.API.Models
{
    public class ResourceCollection<T> : Resource
    {
        public ResourceCollection(List<T> items)
        {
            Items = items;
        }

        public List<T> Items { get; }
    }
}