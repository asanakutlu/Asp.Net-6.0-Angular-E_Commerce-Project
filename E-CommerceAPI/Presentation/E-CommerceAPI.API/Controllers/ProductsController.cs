using E_CommerceAPI.Application.Abstractions;
using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Application.Consts;
using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.Features.Commands.Product.CreateProduct;
using E_CommerceAPI.Application.Features.Commands.Product.ProductImageFile.RemoveProductImage;
using E_CommerceAPI.Application.Features.Commands.Product.ProductImageFile.UploadProductImage;
using E_CommerceAPI.Application.Features.Commands.Product.RemoveProduct;
using E_CommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using E_CommerceAPI.Application.Features.Commands.ProductImageFile;
using E_CommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using E_CommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using E_CommerceAPI.Application.Features.Queries.Product.ProductImageFile;
using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Application.RequestParametrs;
using E_CommerceAPI.Application.Services;
using E_CommerceAPI.Application.ViewModels.Products;
using E_CommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        readonly private IProductReadRepository _productReadRepository;
        readonly private IProductWriterRepository _productWriterRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileService _fileService;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        readonly IStorageServices _storageServices;
        readonly IConfiguration _configuration;
        readonly IMediator _mediator;
        readonly IProdeucttService _prodeucttService;


        public ProductsController(
            IProductReadRepository productReadRepository,
            IProductWriterRepository productWriterRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService,
            IFileWriteRepository fileWriteRepository,
            IFileReadRepository fileReadRepository,
            IProductImageFileWriteRepository productImageFileWriteRepository,
            IProductImageFileReadRepository productImageFileReadRepository,
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IInvoiceFileReadRepository invoiceFileReadRepository,
            IStorageServices storageServices
,
            IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriterRepository = productWriterRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _storageServices = storageServices;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQeuryRequest getAllProductQeuryRequest)
        {
          GetAllProductQueryResponse response= await _mediator.Send(getAllProductQeuryRequest);
            return Ok(response);



            //await Task.Delay(1500);
            //var totalCount = _productReadRepository.GetAll().Count();
            //var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            //{
            //    p.Id,
            //    p.ProductName,
            //    p.ProductStock,
            //    p.ProductPrice,
            //    p.CreateDate
            //}).ToList();
            //return Ok(new
            //{
            //    totalCount,
            //    products
            //});
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response= await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }
        [HttpGet("qrcode/{productId}")]
        public async Task<IActionResult> GetQRCodeToProduct([FromRoute] string productId)
        {
           var data =await _prodeucttService.QRCodeToProductAsync(productId);
            return File(data,"image/png");
        }
        [HttpPut("qrcode")]
        public async Task<IActionResult> UpdateStokQrCodeToProduct([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok(response);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Writing, Definition = "Create product")]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {

           CreateProductCommandResponse response= await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Updating, Definition = "update product")]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response=await _mediator.Send(updateProductCommandRequest);   
            return Ok();
        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Deleting, Definition = "delete product")]
        public async Task<IActionResult> Delete([FromRoute]RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response= await _mediator.Send(removeProductCommandRequest);   
            return Ok();
        }
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Writing, Definition = "upload product file")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.files = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Reading, Definition = "Get Product Image")]
        public async Task<IActionResult> GetProductImage([FromRoute]GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest); 
            return Ok(response);
        }
        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Deleting, Definition = "Remove Product Image")]
        public async Task<IActionResult> DeleteProductImage([FromRoute]RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {   
            removeProductImageCommandRequest.ImageId=imageId;
            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
            return Ok();
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = Application.Enums.ActionType.Updating, Definition = "Change Showcase Image")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
        {
            ChangeShowcaseImageCommandResponse response=await _mediator.Send(changeShowcaseImageCommandRequest);
            return Ok(response); 
        }

    }
}
