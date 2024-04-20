using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Services
{
    public interface IProdeucttService
    {
        Task<byte[]> QRCodeToProductAsync(string productId);
        Task StockUpdateToProductAsync(string productId, int stock);
    }
}
