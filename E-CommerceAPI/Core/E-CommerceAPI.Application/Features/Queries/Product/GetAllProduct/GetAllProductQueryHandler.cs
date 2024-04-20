using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Application.RequestParametrs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQeuryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly ILogger<GetAllProductQueryHandler> _logger;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger<GetAllProductQueryHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _logger = logger;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQeuryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("get logger");
            throw new Exception("hata laindi");
            var totalProductCount = _productReadRepository.GetAll().Count();
            var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Include(p=>p.ProductImageFiles).
                Select(p => new
            {
                p.Id,
                p.ProductName,
                p.ProductStock,
                p.ProductPrice,
                p.CreateDate,
                p.ProductImageFiles
            }).ToList();
            return new()
            {
                Products=products,
                TotalProductCount= totalProductCount
            };
        }
    }
}
