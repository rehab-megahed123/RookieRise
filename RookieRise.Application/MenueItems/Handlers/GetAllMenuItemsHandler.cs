using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RookieRise.Application.MenueItems.DTOS;
using RookieRise.Application.MenueItems.Queries;
using RookieRise.Application.Repositories;
using RookieRise.Data.Contracts;
using RookieRise.Data.Contracts.Result;

namespace RookieRise.Application.MenueItems.Handlers
{
    public class GetAllMenuItemsHandler : IRequestHandler<GetAllMenuItemsQuery, Result<List<MenuItemDto>>>
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;

        public GetAllMenuItemsHandler(IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<MenuItemDto>>> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var menuItems = await _menuItemRepository.GetAllAsync();

            if (menuItems == null || !menuItems.Any())
                return Result.Failure<List<MenuItemDto>>(Error.NotFound("MenuItem.NotFound", "No menu items found."));

            var menuItemDtos = _mapper.Map<List<MenuItemDto>>(menuItems);

            return Result.Success(menuItemDtos);
        }
    }
}
