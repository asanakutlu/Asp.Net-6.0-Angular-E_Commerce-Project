﻿using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Repositories
{
    internal class BasketItemReadRepository : ReadRepository<BasketItem>, IBasketItemReadRepository
    {
        public BasketItemReadRepository(E_CommerceAPIDbContext context) : base(context)
        {
        }
    }
}
