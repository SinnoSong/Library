using System;

namespace Library.Common.Models
{
    public class CategoryVo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Summary { get; set; }

        public CategoryVo(Guid id, string name, string? summary)
        {
            Id = id;
            Name = name;
            Summary = summary;
        }
    }
}
