﻿using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Request;
using Core.Application.Responses;
using Core.Persistance.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Quaries.GetList
{
    public class GetListBrandQuery:IRequest<GetListResponse<GetListBrandListItemDto>>, ICachableRequest
    {
        public PageRequest PageRequest { get; set; }

        public string CacheKey => $"GetListBrandQuery({PageRequest.PageIndex},{PageRequest.PageSize})";

        public bool BypassCache { get; }

        public TimeSpan? SlidingExpiration { get; }

        public class GetListBrandHandler : IRequestHandler<GetListBrandQuery, GetListResponse<GetListBrandListItemDto>>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            public GetListBrandHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListBrandListItemDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
            {
                Paginate<Brand> brands = await _brandRepository.GetListAsync(
                    index:request.PageRequest.PageIndex,
                    size:request.PageRequest.PageSize,
                    cancellationToken:cancellationToken
                    );

                GetListResponse<GetListBrandListItemDto> response = _mapper.Map<GetListResponse<GetListBrandListItemDto>>(brands);
                return response;
            }
        }
    }
}
