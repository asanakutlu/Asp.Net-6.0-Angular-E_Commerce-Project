using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Product.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IStorageServices _storageServices;
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public UploadProductImageCommandHandler(IStorageServices storageServices, IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _storageServices = storageServices;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageServices.UploadAsync("photo-image", request.files);
            Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            //foreach (var r in result)
            //{
            //    product.ProductImageFiles.Add(new()
            //    {

            //        FileName = r.fileName,
            //        Path = r.pathOrContainerName,
            //        Storage = _storageServices.StorageName,
            //        Products = new List<Product>() { product }
            //    });
            //}

            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageServices.StorageName,
                Products = new List<Domain.Entities.Product>() { product }
            }).ToList()); ;
            await _productImageFileWriteRepository.SaveAsync();
            // var datas= await _storageServices.UploadAsync("resource/files", Request.Form.Files);
            //var datas= await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            // _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            // {
            //     FileName = d.fileName,
            //     Path=d.path,
            //      Storage=_storageServices.StorageName
            // }).ToList()); 
            // await _fileWriteRepository.SaveAsync();

            //var datas = await _fileService.UploadAsync("resource/invoices", Request.Form.Files);
            //_invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,
            //Price=new Random().Next()
            //}).ToList());
            //await _invoiceFileWriteRepository.SaveAsync();

            //var datas = await _fileService.UploadAsync("resource/files", Request.Form.Files);
            //_fileWriteRepository.AddRangeAsync(datas.Select(d => new Domain.Entities.File()
            //{
            //    FileName = d.fileName,  
            //    Path = d.path
            //}).ToList());
            //await _fileWriteRepository.SaveAsync();
            //var d1 = _fileReadRepository.GetAll(false);
            //var d2 = _invoiceFileReadRepository.GetAll(false);
            //var d3 = _productImageFileReadRepository.GetAll(false);
            return new();
        }
    }
}
