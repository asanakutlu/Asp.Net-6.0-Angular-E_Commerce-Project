using MediatR;

namespace E_CommerceAPI.Application.Features.Commands.Basket.UptadateQuanlity
{
    public class UpdateQuantityCommandRequest:IRequest<UpdateQuantityCommandResponse>
    {
        public string BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}