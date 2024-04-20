﻿using E_CommerceAPI.Application.Abstractions.Hubs;
using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;

        public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            try { 
           await _orderService.CreateOrderAsync(new()
            {
                Adress = request.Adress,
                Description = request.Description,
                BasketId = _basketService.GetUserActiveBasket?.Id.ToString()
            });
            await _orderHubService.OrderAddedMessageAsync("new order!! :)");
            return new();
            } catch(Exception ex)
            {
                throw;
            }
        }
    }
}
