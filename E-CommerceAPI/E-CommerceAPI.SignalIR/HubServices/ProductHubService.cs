using E_CommerceAPI.Application.Abstractions.Hubs;
using E_CommerceAPI.SignalIR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SignalIR.HubServices
{
    public class ProductHubService : IProductHubService
    {
        readonly IHubContext<ProductHub> _hubContext;

        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProductAddedMessage(string message)
        {
           await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage,message);
        }
    }
}
