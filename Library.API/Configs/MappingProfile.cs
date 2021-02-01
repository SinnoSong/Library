using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using System;

namespace Library.API.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ForMember(dest => dest.Age, config => config.MapFrom(src => DateTime.Now.Year - src.BirthDate.Year));
            CreateMap<Book, BookDto>();
            CreateMap<AuthorForCreationDto, Author>();
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>().ReverseMap();
        }
    }
}