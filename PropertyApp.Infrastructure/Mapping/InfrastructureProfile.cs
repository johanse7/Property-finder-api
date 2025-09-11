using AutoMapper;
using PropertyApp.Domain.Entities;
using PropertyApp.Infrastructure.Models;

namespace PropertyApp.Infrastructure.Mappings
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<PropertyDocument, Property>().ReverseMap();
            CreateMap<PropertyImageDocument, PropertyImage>().ReverseMap();
            CreateMap<OwnerDocument, Owner>().ReverseMap();
            CreateMap<PropertyTraceDocument, PropertyTrace>().ReverseMap();

            CreateMap<PropertyAggregateResult, Property>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
            .ForMember(dest => dest.Traces, opt => opt.MapFrom(src => src.Traces))
            .ReverseMap();
        }
    }
}
