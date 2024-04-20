using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryResponse
    {
        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public long ProductPrice { get; set; }
    }
}
