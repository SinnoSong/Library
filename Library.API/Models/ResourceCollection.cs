using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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