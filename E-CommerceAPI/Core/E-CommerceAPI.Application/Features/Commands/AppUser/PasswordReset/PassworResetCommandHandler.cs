using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.PasswordReset
{
    public class PassworResetCommandHandler : IRequestHandler<PassworResetCommandRequest, PassworResetCommandResponse>
    {
        readonly IAuthService _authService;
        public async Task<PassworResetCommandResponse> Handle(PassworResetCommandRequest request, CancellationToken cancellationToken)
        {
           await _authService.PassworResetAsync(request.Email);
            return new();
        }
    }
}
