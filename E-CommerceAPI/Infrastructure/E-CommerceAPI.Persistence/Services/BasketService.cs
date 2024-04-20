using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Application.ViewModels.Baskets;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Domain.Entities.Identity;
using E_CommerceAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppUser> _userManager;
        readonly IOrderReadRepository _orderReadRepository;
        readonly IBasketWriterRepository _basketWriterRepository;
        readonly IBasketItemWriterRepository _basketItemWriterRepository;
        readonly IBasketItemReadRepository _basketItemReadRepository;
        readonly IBasketReadRepository _basketReadRepository;
        public BasketService(IHttpContextAccessor httpContextAccessor, IOrderReadRepository orderReadRepository, UserManager<AppUser> userManager, IBasketWriterRepository basketWriterRepository, IBasketItemWriterRepository basketItemWriterRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _orderReadRepository = orderReadRepository;
            _userManager = userManager;
            _basketWriterRepository = basketWriterRepository;
            _basketItemWriterRepository = basketItemWriterRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketReadRepository = basketReadRepository;
        }
        private async Task<Basket?> ContextUser()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser? user = await _userManager.Users.Include(p => p.Baskets).FirstOrDefaultAsync(u => u.UserName == username);
                var _basket = from basket in user?.Baskets
                              join order in _orderReadRepository.Table on basket.Id equals order.Id into BasketOrders
                              from order in BasketOrders.DefaultIfEmpty()
                              select new
                              {
                                  Basket = basket,
                                  Order = order
                              };
                Basket? targetBasket = null;
                if (_basket.Any(b => b.Order is null))
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                else
                {
                    targetBasket = new();
                    user.Baskets.Add(targetBasket);

                }
                await _basketWriterRepository.SaveAsync();
                return targetBasket;
            }
            throw new Exception("beklenmedik hata alindi");
        }
        public async Task AddItemToBasketAync(VM_Create_BasketITem basketITem)
        {
            Basket? basket = await ContextUser();
            if (basket != null)
            {
                BasketItem _basketItem = await _basketItemReadRepository.GetSingleAsync(b => b.BasketId == basket.Id && b.ProductId == Guid.Parse(basketITem.ProducId));
                if (_basketItem != null)
                    _basketItem.Quanlity++;
                else
                    await _basketItemWriterRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketITem.ProducId),
                        Quanlity = basketITem.Quanlity
                    });
                await _basketItemWriterRepository.SaveAsync();
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            Basket? basket = await ContextUser();
            Basket? result = await _basketReadRepository.Table.Include(b => b.BasketItems).ThenInclude(c => c.Product).FirstOrDefaultAsync(a => a.Id == basket.Id);
            return result.BasketItems.ToList();

        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                _basketItemWriterRepository.Remove(basketItem);
                await _basketItemWriterRepository.SaveAsync();
            }
        }

        public async Task UpdateQuanlityAsync(VM_Update_BasketItem basketItem)
        {
            BasketItem? _basketItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);
            if (_basketItem != null)
            {
                _basketItem.Quanlity = basketItem.Quanlity;
                await _basketItemWriterRepository.SaveAsync();
            }
        }

        public Basket? GetUserActiveBasket
        {
            get { 
            Basket? basket =ContextUser().Result;
            return basket;
            }
        }
    }
}
