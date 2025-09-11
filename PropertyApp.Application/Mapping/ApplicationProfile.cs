using AutoMapper;
using PropertyApp.Application.Dtos;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Mappings
{
  public class ApplicationProfile : Profile
  {
    public ApplicationProfile()
    {
      CreateMap<Property, PropertyListDto>()
          .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.Images.FirstOrDefault(i => i.Enabled).File));

    CreateMap<Property, PropertyDto>();
      CreateMap<Property, PropertyDetailDto>()
                .ForMember(dest => dest.Property, opt => opt.MapFrom(src => src)) 
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Traces, opt => opt.MapFrom(src => src.Traces));

      CreateMap<PropertyImage, PropertyImageDto>();
      CreateMap<PropertyTrace, PropertyTraceDto>();
      CreateMap<Owner, OwnerDto>();
    }
  };
}