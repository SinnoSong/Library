using AutoMapper;
using Library.API.Entities;
using Library.API.Models;

namespace Library.API.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>().ReverseMap();
            CreateMap<Category, CategoryVo>().ReverseMap();
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<LendConfig, LendConfigVo>().ReverseMap();
            CreateMap<LendConfigForCreationDto, LendConfig>();
        }
    }
}