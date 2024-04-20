using MediatR;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.Verify
{
    public class VerifyResetTokenCommandRequest:IRequest<VerifyResetTokenCommandResponse>
    {
        public string ResetToken { get; set; }
        public string UserId { get; set; }
    }
}