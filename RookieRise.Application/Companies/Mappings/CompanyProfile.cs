using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RookieRise.Application.Companies.DTOS;
using RookieRise.Data.Entities;

namespace RookieRise.Application.Companies.Mappings
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>();
        }
    }
}
