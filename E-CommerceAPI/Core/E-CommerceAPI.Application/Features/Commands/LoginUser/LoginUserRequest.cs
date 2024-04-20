using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.LoginUser
{
    public class LoginUserRequest:IRequest<LoginUserResponse>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
