using E_CommerceAPI.Application.Abstractions;
using E_CommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        => new()
        {
            new (){Id=Guid.NewGuid(),ProductName="Product 1",ProductPrice=100,ProductStock=10},
            new (){Id=Guid.NewGuid(),ProductName="Product 2",ProductPrice=200,ProductStock=20},
            new (){Id=Guid.NewGuid(),ProductName="Product 3",ProductPrice=300,ProductStock=30},
            new (){Id=Guid.NewGuid(),ProductName="Product 4",ProductPrice=400,ProductStock=40},
            new (){Id=Guid.NewGuid(),ProductName="Product 5",ProductPrice=500,ProductStock=50}
        };
    }
}
