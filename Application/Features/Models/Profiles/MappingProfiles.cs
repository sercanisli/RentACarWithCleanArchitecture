using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Model,GetListModelListItemDto>()
                .ForMember(destinationMember: m => m.BrandName, memberOptions: option => option.MapFrom(m => m.Brand.Name))
                .ForMember(destinationMember: m => m.FuelName, memberOptions: option => option.MapFrom(m => m.Fuel.Name))
                .ForMember(destinationMember: m => m.TransmissionName, memberOptions: option => option.MapFrom(m => m.Transmission.Name))
                .ReverseMap();

            CreateMap<Model, GetListByDynamicModelListItemDto>()
               .ForMember(destinationMember: m => m.BrandName, memberOptions: option => option.MapFrom(m => m.Brand.Name))
               .ForMember(destinationMember: m => m.FuelName, memberOptions: option => option.MapFrom(m => m.Fuel.Name))
               .ForMember(destinationMember: m => m.TransmissionName, memberOptions: option => option.MapFrom(m => m.Transmission.Name))
               .ReverseMap();

            CreateMap<Paginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
            CreateMap<Paginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();
        }
    }
}
