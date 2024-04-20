﻿using MediatR;

namespace E_CommerceAPI.Application.Features.Commands.Basket.RemoveBasketItem
{
    public class RemoveBasketItemCommandRequest:IRequest<RemoveBasketItemCommandResponse>
    {
        public string BasketITemId { get; set; }
    }
}