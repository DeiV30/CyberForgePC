namespace  cyberforgepc.Controllers.User
{
    using cyberforgepc.BusinessLogic;
    using cyberforgepc.Helpers.Common;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Helpers.Middleware;
    using cyberforgepc.Models.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUsers userManager;

        public UserController(IUsers userManager) => this.userManager = userManager;


        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await userManager.GetAll();
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

     
        [HttpGet("{id}")]
        [AuthorizeMultiple(Role.Admin, Role.Client)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await userManager.GetById(id);
                return Ok(new { data = result });
            }
            catch (MessageException ex)
            {
                return NotFound(new { code = ex.ExceptionCode, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpPost]
        [ValidationModel]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] UserInsertRequest request)
        {
            try
            {
                var result = await userManager.Create(request);
                return Ok(new { data = result });
            }
            catch (MessageException ex)
            {
                return Conflict(new { code = ex.ExceptionCode, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        [ValidationModel]
        [Authorize(Roles = Role.Client)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Put(string id, [FromBody] UserUpdateRequest request)
        {
            try
            {
                await userManager.Update(id, request);
                return Ok(new { data = true });
            }
            catch (MessageException ex)
            {
                return NotFound(new { code = ex.ExceptionCode, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
