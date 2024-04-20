using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace E_CommerceAPI.Application.Features.Commands.Product.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriterRepository _productWriterRepository;

        public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriterRepository productWriterRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriterRepository = productWriterRepository;
        }

        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            Domain.Entities.ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
            if(productImageFile != null) 
            product?.ProductImageFiles.Remove(productImageFile);
            await _productWriterRepository.SaveAsync();
            return new();
        }
    }
}
