using E_CommerceAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator: AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("ürün adını gir").MaximumLength(150).MinimumLength(5).WithMessage("lütfen 5 ie 150 karekter arasında olsn");
            RuleFor(p => p.Stock).NotEmpty().NotNull().WithMessage("boş geçme").Must(s => s >= 0).WithMessage("stok bilgisi negatif olamaz");
            RuleFor(p => p.Price).NotEmpty().NotNull().WithMessage("boş geçme").Must(s => s >= 0).WithMessage("fiyat bilgisi negatif olamaz");


        }
    }
}
