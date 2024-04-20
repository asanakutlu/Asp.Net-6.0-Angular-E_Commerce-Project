using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.AssingRoleToUser
{
    public class AssingRoleToUserCommandHandler : IRequestHandler<AssingRoleToUserCommandRequest, AssingRoleToUserCommandReaponse>
    {
        readonly IUserService _userService;

        public AssingRoleToUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AssingRoleToUserCommandReaponse> Handle(AssingRoleToUserCommandRequest request, CancellationToken cancellationToken)
        {
           await _userService.AssignRoleToUserAsync(request.UserId, request.Roles);
            return new();
        }
    }
}
