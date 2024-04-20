using E_CommerceAPI.Application.Abstractions.Services.Configurations;
using E_CommerceAPI.Application.CustomAttributes;
using E_CommerceAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ApplicationServiceController : ControllerBase
    {
        readonly IApplicationService _applicationService;

        public ApplicationServiceController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType =ActionType.Reading,Definition ="Get Authorize Definition Endpoinds",Menu ="Application Service")]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
           var datas= _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));

            return Ok(datas);
        }
    }
}
