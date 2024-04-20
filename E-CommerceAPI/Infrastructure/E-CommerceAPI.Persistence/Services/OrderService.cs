using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Order;
using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;
        readonly ICompetedOrderWriteRepository _competedOrderWriteRepository;
        readonly ICompetedOrderReadRepository _competedOrderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompetedOrderWriteRepository competedOrderWriteRepository, ICompetedOrderReadRepository competedOrderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _competedOrderWriteRepository = competedOrderWriteRepository;
            _competedOrderReadRepository = competedOrderReadRepository;
        }


        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);
            await _orderWriteRepository.AddAsync(new()
            {

                Adress = createOrder.Adress,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = orderCode

            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(x => x.Basket).ThenInclude(b => b.User).Include
                 (o => o.Basket).ThenInclude(o => o.BasketItems).ThenInclude(o => o.Product);
            
            var data = query.Skip(page * size).Take(size);
            //var data = query.Take((page * size)..size);
            var data2= from order in query
                        join completedOrder in _competedOrderReadRepository.Table on order.Id equals completedOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            order.Id,
                            order.CreateDate,
                            order.OrderCode,
                            order.Basket,
                            Completed = _co != null ? true : false,

                        };

            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data2.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CreateDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(o => o.Product.ProductPrice * o.Quanlity),
                    UserName = o.Basket.User.UserName,
                    o.Completed  
                }).ToListAsync()
            };
        }

        async Task<SingleOrder> IOrderService.GetOrderByIdAync(string id)
        {
            var data =  _orderReadRepository.Table.Include(o => o.Basket).ThenInclude(o => o.BasketItems).ThenInclude(o => o.Product);
            var data2= await (from order in data
                      join completedOrder in _competedOrderReadRepository.Table on order.Id equals completedOrder.OrderId into co
                      from _co in co.DefaultIfEmpty()
                      select new
                      {
                          Id=order.Id,
                         CreatedDate= order.CreateDate,
                          OrderCode=order.OrderCode,
                         Basket= order.Basket,
                          Completed = _co != null ? true : false,
                          Adress=order.Adress,
                          Descripton=order.Description

                      }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            return new()
            {
                Id = data2.Id.ToString(),
                BasketItems = data2.Basket.BasketItems.Select(o => new
                {
                    o.Product.ProductName,
                    o.Product.ProductPrice,
                    o.Quanlity
                }),
                Address = data2.Adress,
                CreatedDate = data2.CreatedDate,
                Description = data2.Descripton,
                OrderCode = data2.OrderCode,
                Completed=data2.Completed
            };
        }
        public async Task<(bool, CompletedOrderDTO)> CompletedOrderAsync(string id)
        {
            Order? order = await _orderReadRepository.Table.Include(o=>o.Basket).ThenInclude(o=>o.User).FirstOrDefaultAsync(o=>o.Id==Guid.Parse(id));
            if (order != null)
            {
                await _competedOrderWriteRepository.AddAsync(new() { OrderId = Guid.Parse(id), });
                return (await _competedOrderWriteRepository.SaveAsync()>0,new ()
                {
                    OrderCode=order.OrderCode,
                    OrderDate=order.CreateDate,
                    UserName=order.Basket.User.UserName,
                    UserSurname=order.Basket.User.NameSurname,
                    Email=order.Basket.User.Email
                });
            }
            return (false,null);
        }

    }
}
