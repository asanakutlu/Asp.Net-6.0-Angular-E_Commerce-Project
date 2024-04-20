using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Application.ViewModels.Products;
using E_CommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Services
{
    public class ProducttService : IProdeucttService
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriterRepository _productWriterRepository;
        readonly IQRCodeService _qrCodeService;

        public ProducttService(IProductReadRepository productReadRepository, IQRCodeService qrCodeService = null, IProductWriterRepository productWriterRepository = null)
        {
            _productReadRepository = productReadRepository;
            _qrCodeService = qrCodeService;
            _productWriterRepository = productWriterRepository;
        }

        public async Task<byte[]> QRCodeToProductAsync(string productId)
        {
            Product product = await _productReadRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("product no found");
            var plainObejct = new
            {
                product.Id,
                product.ProductName,
                product.ProductPrice,
                product.ProductStock,
                product.CreateDate
            };
            string plainText=JsonSerializer.Serialize(plainObejct);
            return _qrCodeService.GenerateQRCode(plainText);
        }

        public async Task StockUpdateToProductAsync(string productId, int stock)
        {
            Product product = await _productReadRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("product no");
            product.ProductStock = stock;
            await _productWriterRepository.SaveAsync();  
        }
    }
}
