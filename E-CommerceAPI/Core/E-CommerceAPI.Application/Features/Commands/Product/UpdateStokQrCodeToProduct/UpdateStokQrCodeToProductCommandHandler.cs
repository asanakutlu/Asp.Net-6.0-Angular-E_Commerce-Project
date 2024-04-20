using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Product.UpdateStokQrCodeToProduct
{
    public class UpdateStokQrCodeToProductCommandHandler : IRequestHandler<UpdateStokQrCodeToProductCommandRequest, UpdateStokQrCodeToProductCommandResponse>
    {
        readonly IProdeucttService _prodeucttService;

        public UpdateStokQrCodeToProductCommandHandler(IProdeucttService prodeucttService)
        {
            _prodeucttService = prodeucttService;
        }

        public async Task<UpdateStokQrCodeToProductCommandResponse> Handle(UpdateStokQrCodeToProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _prodeucttService.StockUpdateToProductAsync(request.ProductId,request.Stok);
            return new(); 
        }
    }
}
