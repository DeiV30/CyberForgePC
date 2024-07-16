namespace cyberforgepc.Controllers.Authentication
{
    using cyberforgepc.BusinessLogic;
    using cyberforgepc.Helpers.Common;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Helpers.Middleware;
    using cyberforgepc.Models.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUsers userManager;

        public AuthenticationController(IUsers userManager) => this.userManager = userManager;


        [HttpPost("loginEmail")]
        [ValidationModel]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostAuth([FromBody] UserLoginRequest request)
        {
            try
            {
                var user = await userManager.Authenticate(request);
                return Ok(new { data = user });
            }
            catch (MessageException ex)
            {
                return StatusCode(403, new { code = ex.ExceptionCode, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpPost("refresh")]
        [ValidationModel]
        [AuthorizeMultiple(Role.Admin, Role.Client)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var result = await userManager.RefreshToken(request);
                return Ok(new { data = result });
            }
            catch (MessageException ex)
            {
                return StatusCode(403, new { code = ex.ExceptionCode, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}
