using MediatR;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.AssingRoleToUser
{
    public class AssingRoleToUserCommandRequest:IRequest<AssingRoleToUserCommandReaponse>
    {
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
} 