using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
          var data= await _orderService.GetOrderByIdAync(request.Id);
            return new()
            {
                Id = data.Id.ToString(),
                BasketItems = data.BasketItems,
                Address = data.Address,
                CreatedDate = data.CreatedDate,
                Description = data.Description,
                OrderCode = data.OrderCode,
                Completed=data.Completed,
            };
        }
    }
}
