﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.DTOs.Order
{
    public class CreateOrder
    {
        public string? BasketId { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
    }
}
