using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommand:IRequest<CreatedBrandResponse>, ITransactionalRequest, ICacheRemoverRequest
    {
        public string Name { get; set; }

        public string CacheKey => throw new NotImplementedException();

        public bool BypassCache => throw new NotImplementedException();

        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRoles _brandBusinessRoles;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRoles brandBusinessRoles)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRoles = brandBusinessRoles;
            }

            public async Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                await _brandBusinessRoles.BrandNameCannotBeDuplicatedWhenInserted(request.Name);

                Brand brand = _mapper.Map<Brand>(request);
                brand.Id = Guid.NewGuid();

                await _brandRepository.AddAsync(brand);

                CreatedBrandResponse response = _mapper.Map<CreatedBrandResponse>(brand);
                return response;
            }
        }
    }
}
