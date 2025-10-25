using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RookieRise.Application.MenueItems.DTOS;
using RookieRise.Data.Contracts.Result;

namespace RookieRise.Application.MenueItems.Queries
{
    public record GetAllMenuItemsQuery() : IRequest<Result<List<MenuItemDto>>>;
}
