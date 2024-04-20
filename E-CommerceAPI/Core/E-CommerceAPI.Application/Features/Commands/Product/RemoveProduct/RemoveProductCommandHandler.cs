using E_CommerceAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriterRepository _productWriterRepository;

        public RemoveProductCommandHandler(IProductReadRepository productReadRepository, IProductWriterRepository productWriterRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriterRepository = productWriterRepository;
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriterRepository.RemoveAsync(request.Id);
            await _productWriterRepository.SaveAsync();
            return new();
        }
    }
}
