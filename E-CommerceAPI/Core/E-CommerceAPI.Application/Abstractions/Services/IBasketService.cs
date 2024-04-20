using E_CommerceAPI.Application.ViewModels.Baskets;
using E_CommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Services
{
    public interface IBasketService
    {

        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddItemToBasketAync(VM_Create_BasketITem basketITem);
        public Task UpdateQuanlityAsync(VM_Update_BasketItem basketItem);
        public Task RemoveBasketItemAsync(string basketItemId);
        public Basket? GetUserActiveBasket { get; }
    }
}
