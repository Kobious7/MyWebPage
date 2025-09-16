using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WithDB.Common.Responses;

namespace WithDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("adminpage")]
        public IActionResult GotoPrimarionWorld()
            => Ok(ApiResponse<string>.Ok("Admin Page"));
    }
}
