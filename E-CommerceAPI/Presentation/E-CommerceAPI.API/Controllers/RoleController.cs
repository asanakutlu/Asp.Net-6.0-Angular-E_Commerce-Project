using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.Enums;
using E_CommerceAPI.Application.Features.Queries.Role.CreateRole;
using E_CommerceAPI.Application.Features.Queries.Role.DeleteRole;
using E_CommerceAPI.Application.Features.Queries.Role.GetRoleById;
using E_CommerceAPI.Application.Features.Queries.Role.GetRoles;
using E_CommerceAPI.Application.Features.Queries.Role.UpdateRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RoleController : ControllerBase
    {
        readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType =ActionType.Reading,Definition ="Get Roles",Menu ="Roles")]
        public async Task<IActionResult> GetRoles([FromQuery] GetRolesQueryRequest getRolesQueryRequest)
        {
            GetRolesQueryResponse response = await _mediator.Send(getRolesQueryRequest);
            return Ok(response);
        }
        [HttpGet("{id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Role By Id", Menu = "Roles")]
        public async Task<IActionResult> GetRoles([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            GetRoleByIdQueryResponse response = await _mediator.Send(getRoleByIdQueryRequest);
            return Ok(response);
        }
        [HttpPost()]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Role", Menu = "Roles")]
        public async Task<IActionResult> CreateRole([FromBody]CreateRoleQueryRequest createRoleQueryRequest)
        {
            CreateRoleQueryResponse response=await _mediator.Send(createRoleQueryRequest);
            return Ok(response);
        }
        [HttpPut("{id}")]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "uppdate Role", Menu = "Roles")]
        public async Task<IActionResult> UpdateRole([FromBody,FromRoute] UpdateRoleQueryRequest updateRoleQueryRequest)
        {
            UpdateRoleQueryResponse response = await _mediator.Send(updateRoleQueryRequest);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Role ", Menu = "Roles")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleQueryRequest deleteRoleQueryRequest)
        {
            DeleteRoleQueryResponse response = await _mediator.Send(deleteRoleQueryRequest);
            return Ok(response);
        }
    }
}
