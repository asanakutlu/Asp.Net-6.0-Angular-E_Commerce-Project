using E_CommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommanfHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriterRepository _productWriterRepository;
        readonly ILogger<UpdateProductCommanfHandler> _logger;

        public UpdateProductCommanfHandler(IProductReadRepository productReadRepository, IProductWriterRepository productWriterRepository, ILogger<UpdateProductCommanfHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _productWriterRepository = productWriterRepository;
            _logger = logger;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            E_CommerceAPI.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);
            product.ProductStock = request.Stock;
            product.ProductPrice = request.Price;
            product.ProductName = request.Name;
            await _productWriterRepository.SaveAsync();
            _logger.LogInformation("Product update...");
            return new();
        }
    }
}
