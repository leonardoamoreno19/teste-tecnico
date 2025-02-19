using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleResponse<T>(T data)
        {
            if (data == null)
                return NotFound();

            return Ok(data);
        }
    }
} 