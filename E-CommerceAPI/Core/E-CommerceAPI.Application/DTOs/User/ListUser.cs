﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.DTOs.User
{
    public class ListUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        public bool TwoFactoryEnabled { get; set; }
        public string UserName { get; set; }
    }
   
}
