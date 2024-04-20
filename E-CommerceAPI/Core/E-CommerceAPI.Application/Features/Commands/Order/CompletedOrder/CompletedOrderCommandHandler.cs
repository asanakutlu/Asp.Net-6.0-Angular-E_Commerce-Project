using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace E_CommerceAPI.Application.Features.Commands.Order.CompletedOrder
{
    public class CompletedOrderCommandHandler : IRequestHandler<CompletedOrderCommandRequest, CompletedOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IMailService _mailService;

        public CompletedOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompletedOrderCommandResponse> Handle(CompletedOrderCommandRequest request, CancellationToken cancellationToken)
        {
           (bool succeded,CompletedOrderDTO dto) = await _orderService.CompletedOrderAsync(request.Id);
            if (succeded)
                await _mailService.SendComletedOrdeMailAsync(dto.Email, dto.OrderCode,dto.OrderDate,dto.UserName);
            return new();
        }
    }
}
