using MediatR;

namespace E_CommerceAPI.Application.Features.Commands.Order.CompletedOrder
{
    public class CompletedOrderCommandRequest:IRequest<CompletedOrderCommandResponse>
    {
        public string Id { get; set; }
    }
}