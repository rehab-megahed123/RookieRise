using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RookieRise.Application.Companies.DTOS;
using RookieRise.Application.Companies.Queries;
using RookieRise.Application.Repositories;
using RookieRise.Data.Contracts;
using RookieRise.Data.Contracts.Result;



namespace RookieRise.Application.Companies.Handlers
{
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, Result<List<CompanyDto>>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<CompanyDto>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetAllAsync();

            if (companies == null || !companies.Any())
                return Result.Failure<List<CompanyDto>>(new Error("Company.NotFound", "No companies found."));

            var result = _mapper.Map<List<CompanyDto>>(companies);
            return Result.Success(result);
        }
    }
}
