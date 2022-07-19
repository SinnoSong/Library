using AntDesign.ProLayout;
using AutoMapper;
using Library.Common.Models;

namespace Library.Web.Config;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<NoticeNoContentVo, NoticeIconData>()
            .ForMember(d => d.Datetime, opt => opt.MapFrom(src => src.CreateTime));
        CreateMap<CategoryDto, CategoryCreateDto>();
        CreateMap<BookDto, BookUpdateDto>();
        CreateMap<LendConfigDto, LendConfigCreateDto>();
    }
}