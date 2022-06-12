﻿using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using System;

namespace Library.API.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>().ReverseMap();
        }
    }
}