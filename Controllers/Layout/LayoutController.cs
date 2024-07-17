namespace cyberforgepc.Controllers.Layout
{
    using cyberforgepc.BusinessLogic;
    using cyberforgepc.Helpers.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [Authorize(Roles = Role.Admin)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LayoutController : ControllerBase
    {
        private readonly ILayout layoutManager;

        public LayoutController(ILayout layoutManager) => this.layoutManager = layoutManager;

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await layoutManager.GetCountResume();
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("inventory")]
        [Authorize(Roles = Role.Admin)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetInventory()
        {
            try
            {
                var result = await layoutManager.GetAll();
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
