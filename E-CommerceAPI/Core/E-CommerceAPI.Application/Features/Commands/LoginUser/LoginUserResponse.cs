using E_CommerceAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.LoginUser
{
    public class LoginUserResponse
    {
    }
    public class LoginUserSuccessCommandResponse: LoginUserResponse
    {
        public Token Token { get; set; }
    }
    public class LoginUserErorrCommandResponse: LoginUserResponse
    {
        public string Message { get; set; }
    }
}
