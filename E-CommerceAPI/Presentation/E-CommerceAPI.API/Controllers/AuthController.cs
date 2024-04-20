using E_CommerceAPI.Application.Features.Commands.AppUser.CreateUser.FacebookLogin;
using E_CommerceAPI.Application.Features.Commands.AppUser.CreateUser.GoogleLogin;
using E_CommerceAPI.Application.Features.Commands.AppUser.CreateUser.RefreshTokenLogin;
using E_CommerceAPI.Application.Features.Commands.AppUser.PasswordReset;
using E_CommerceAPI.Application.Features.Commands.AppUser.Verify;
using E_CommerceAPI.Application.Features.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserRequest loginUserRequest)
        {
            LoginUserResponse response = await _mediator.Send(loginUserRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromQuery]RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacobookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest)
        {
            FacebookLoginCommandResponse response = await _mediator.Send(facebookLoginCommandRequest);
            return Ok(response);
        }
        [HttpPost("pasword-reset")]
        public async Task<IActionResult> PasswordReset([FromBody]PassworResetCommandRequest passworResetCommandRequest)
        {
            PassworResetCommandResponse response = await _mediator.Send(passworResetCommandRequest);
            return Ok(response);
        }
        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
        {
            VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);
            return Ok(response);
        }
    }
}
