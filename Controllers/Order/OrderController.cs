namespace cyberforgepc.Controllers.Order
{
    using cyberforgepc.BusinessLogic;
    using cyberforgepc.Helpers.Common;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Helpers.Middleware;
    using cyberforgepc.Models.Order;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrders orderManager;

        public OrderController(IOrders orderManager) => this.orderManager = orderManager;


        [HttpGet("{id}")]
        [Authorize(Roles = Role.Client)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await orderManager.GetByIdList(id);
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

        [HttpGet("{id}/orderitem")]
        [Authorize(Roles = Role.Client)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByIdOrderList(string id)
        {
            try
            {
                var result = await orderManager.GetByIdOrderList(id);
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
        [Authorize(Roles = Role.Client)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] OrderRequest request)
        {
            try
            {
                var result = await orderManager.Create(request);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpPut("{id}/{state}")]
        [ValidationModel]
        [Authorize(Roles = Role.Admin)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> State(string id, string state)
        {
            try
            {
                var result = await orderManager.State(id, state);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}
