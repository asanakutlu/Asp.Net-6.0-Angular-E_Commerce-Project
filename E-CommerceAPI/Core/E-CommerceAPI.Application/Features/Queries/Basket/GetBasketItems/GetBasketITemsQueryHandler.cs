using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketITemsQueryHandler : IRequestHandler<GetBasketITemsQueryRequest, List<GetBasketITemsQueryResponse>>
    {
        readonly IBasketService _basketService;

        public GetBasketITemsQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetBasketITemsQueryResponse>> Handle(GetBasketITemsQueryRequest request, CancellationToken cancellationToken)
        {
            var basketItems=await _basketService.GetBasketItemsAsync();
            return basketItems.Select(b => new GetBasketITemsQueryResponse
            {
                BasketItemId = b.Id.ToString(),
                Name = b.Product.ProductName,
                Price = b.Product.ProductPrice,
                Quatity = b.Quanlity

            }).ToList();
        }
    }
}
