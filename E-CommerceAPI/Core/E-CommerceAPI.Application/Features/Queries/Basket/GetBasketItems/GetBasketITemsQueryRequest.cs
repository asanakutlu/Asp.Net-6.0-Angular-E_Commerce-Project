using MediatR;

namespace E_CommerceAPI.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketITemsQueryRequest:IRequest<List<GetBasketITemsQueryResponse>>
    {
    }
}