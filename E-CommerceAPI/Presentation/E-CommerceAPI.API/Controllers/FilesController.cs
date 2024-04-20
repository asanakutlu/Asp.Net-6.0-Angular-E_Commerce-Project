using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : Controller
    {
        readonly IConfiguration _configuration;
       
        public FilesController(IConfiguration configuration) => _configuration = configuration;
        [HttpGet("action")]
        public IActionResult GetBaseStrogeUrl()
        {
            return Ok( new { Url=_configuration["BaseStrorageUrl"] });
        }
    }
}
