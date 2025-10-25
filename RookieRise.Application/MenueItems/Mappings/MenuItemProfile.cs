using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RookieRise.Application.MenueItems.DTOS;
using RookieRise.Data.Entities;

namespace RookieRise.Application.MenueItems.Mappings
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem, MenuItemDto>().ReverseMap();
        }
    }
}
