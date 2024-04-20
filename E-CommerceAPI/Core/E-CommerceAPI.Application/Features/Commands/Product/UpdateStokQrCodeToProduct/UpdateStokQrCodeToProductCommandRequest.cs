using MediatR;
using System.Security.Permissions;

namespace E_CommerceAPI.Application.Features.Commands.Product.UpdateStokQrCodeToProduct
{
    public class UpdateStokQrCodeToProductCommandRequest:IRequest<UpdateStokQrCodeToProductCommandResponse>
    {
        public string ProductId { get; set; }
        public int Stok { get; set; }
    }
}