using MediatR;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.PasswordReset
{
    public class PassworResetCommandRequest:IRequest<PassworResetCommandResponse>
    {
        public string Email { get; set; }
    }
}