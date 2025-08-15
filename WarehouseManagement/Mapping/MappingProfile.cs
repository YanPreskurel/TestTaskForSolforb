using AutoMapper;
using WarehouseManagement.Models.DTOs;
using WarehouseManagement.Models.Entities;

namespace WarehouseManagement.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Resource, ResourceReadDto>();
            CreateMap<ResourceCreateDto, Resource>();
            CreateMap<ResourceUpdateDto, Resource>();


            CreateMap<Unit, UnitReadDto>();
            CreateMap<UnitCreateDto, Unit>();
            CreateMap<UnitUpdateDto, Unit>();

            CreateMap<ReceiptDocument, ReceiptDocumentReadDto>()
                .ForMember(dest => dest.Resources, opt => opt.MapFrom(src => src.ReceiptResources));
            CreateMap<ReceiptDocumentCreateDto, ReceiptDocument>()
                .ForMember(d => d.ReceiptResources, o => o.MapFrom(s => s.Resources));
            CreateMap<ReceiptResourceCreateDto, ReceiptResource>();
            CreateMap<ReceiptDocumentUpdateDto, ReceiptDocument>();


            CreateMap<ReceiptResource, ReceiptResourceReadDto>()
                .ForMember(dest => dest.ResourceName, opt => opt.MapFrom(src => src.Resource.Name))
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit.Name));
            CreateMap<ReceiptResourceCreateDto, ReceiptResource>();
            CreateMap<ReceiptResourceUpdateDto, ReceiptResource>();
        }
    }
}
