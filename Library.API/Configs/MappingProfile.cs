using AutoMapper;
using Library.API.Entities;
using Library.Common.Models;

namespace Library.API.Configs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<BookCreateDto, Book>();
        CreateMap<BookUpdateDto, Book>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<LendConfig, LendConfigDto>().ReverseMap();
        CreateMap<LendConfigCreateDto, LendConfig>();
        CreateMap<LendRecord, LendRecordDto>().ReverseMap();
        CreateMap<LendRecordCreateDto, LendRecord>();
        CreateMap<User, UserDto>();
    }
}