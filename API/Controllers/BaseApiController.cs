using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route(template: "api/[controller]/[action]")]
    public class BaseApiController : ControllerBase { }
}
