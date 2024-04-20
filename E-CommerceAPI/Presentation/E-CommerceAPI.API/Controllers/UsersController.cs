using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.Enums;
using E_CommerceAPI.Application.Features.Commands.AppUser.AssingRoleToUser;
using E_CommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using E_CommerceAPI.Application.Features.Commands.AppUser.CreateUser.FacebookLogin;
using E_CommerceAPI.Application.Features.Commands.AppUser.CreateUser.GoogleLogin;
using E_CommerceAPI.Application.Features.Commands.AppUser.UpdatePassword;
using E_CommerceAPI.Application.Features.Commands.LoginUser;
using E_CommerceAPI.Application.Features.Queries.AppUser;
using E_CommerceAPI.Application.Features.Queries.AppUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommandRequest createUserRequest)
        {
            CreateUserCommandResponse response=await _mediator.Send(createUserRequest);
            return Ok(response);
        }
        [HttpPost("update-pasword")]
        public async Task<IActionResult> UpdatePassword([FromBody]UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }
        [HttpGet]
        [AuthorizeDefinition(ActionType =ActionType.Reading,Definition ="Get All Users",Menu ="Users")]
        public async Task<IActionResult> GetAllUsers(GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }
        [HttpGet("get-roles-to-user/{userId}")]
        [Authorize(AuthenticationSchemes ="Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles To Users", Menu = "Users")]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest getRolesToUserQueryRequest)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(getRolesToUserQueryRequest); 
            return Ok(response);
        } 
        [HttpPost ("assing-role-to-user")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Assing Role To Users", Menu = "Users")]
        public async Task<IActionResult> AssingRoleToUser(AssingRoleToUserCommandRequest toUserCommandRequest)
        {
            AssingRoleToUserCommandReaponse response = await _mediator.Send(toUserCommandRequest);
            return Ok(response);
        }



    }
}
