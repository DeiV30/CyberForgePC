namespace cyberforgepc.Controllers.Product
{
    using cyberforgepc.BusinessLogic;
    using cyberforgepc.Helpers.Common;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Helpers.Middleware;
    using cyberforgepc.Models.Product;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Role.Admin)]
    [Produces("application/json")]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IInventoryTransactions inventoryTransactionsManager;

        public InventoryTransactionController(IInventoryTransactions inventoryTransactionsManager) => this.inventoryTransactionsManager = inventoryTransactionsManager;


        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await inventoryTransactionsManager.GetById(id);
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
    }
}
