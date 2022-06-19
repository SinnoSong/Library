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
            CreateMap<LendRecord, LendRecordVo>().ReverseMap();
            CreateMap<LendRecordForCreationDto, LendRecord>();
            CreateMap<Notice, NoticeVo>().ReverseMap();
            CreateMap<Notice, NoticeNoContentVo>();
            CreateMap<NoticeForCreationDto, Notice>();
        }
    }
}