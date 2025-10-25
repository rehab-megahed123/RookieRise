using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RookieRise.Application.Companies.DTOS;
using RookieRise.Data.Contracts.Result;

namespace RookieRise.Application.Companies.Queries
{
    public class GetAllCompaniesQuery : IRequest<Result<List<CompanyDto>>>
    {
    }
}
