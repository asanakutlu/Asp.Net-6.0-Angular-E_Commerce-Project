using E_CommerceAPI.Application.Abstractions.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SignalIR.HubServices
{
    public class OrderHubService : IOrderHubService
    {
        readonly IHubContext _hubContext;

        public OrderHubService(IHubContext hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OrderAddedMessageAsync(string message)
          => await _hubContext.Clients.All.SendAsync (ReceiveFunctionNames.OrderAddedMessage,message);
        
    }
}
