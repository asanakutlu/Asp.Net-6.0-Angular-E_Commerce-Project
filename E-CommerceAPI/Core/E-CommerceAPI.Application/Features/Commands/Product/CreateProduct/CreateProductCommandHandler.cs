using E_CommerceAPI.Application.Abstractions.Hubs;
using E_CommerceAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriterRepository _productWriterRepository;
        readonly IProductHubService _productHubService;
        public CreateProductCommandHandler(IProductWriterRepository productWriterRepository, IProductHubService productHubService)
        {
            _productWriterRepository = productWriterRepository;
            _productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriterRepository.AddAsync(new()
            {
                ProductName = request.Name,
                ProductPrice = request.Price,
                ProductStock = request.Stock
            });
            await _productWriterRepository.SaveAsync();
            _productHubService.ProductAddedMessage($"{request.Name} isminde ürün eklenmiştir. ");
            return new();
        }
    }
}
