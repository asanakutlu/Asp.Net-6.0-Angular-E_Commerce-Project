using E_CommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Domain.Entities
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public long ProductPrice { get; set; }
        //public ICollection<Order> Orders { get; set;}
        public ICollection<ProductImageFile> ProductImageFiles { get;}
        public ICollection<BasketItem> BasketItems { get; set;}
    }
}
