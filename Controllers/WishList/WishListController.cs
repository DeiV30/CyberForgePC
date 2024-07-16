namespace  cyberforgepc.Controllers.WishList
{
    using cyberforgepc.BusinessLogic;
    using cyberforgepc.Database.Models;
    using cyberforgepc.Helpers.Common;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Helpers.Middleware;
    using cyberforgepc.Models.Coupon;
    using cyberforgepc.Models.WishList;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [ApiController]
    [Authorize(Roles = Role.Client)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WishListController : ControllerBase
    {
        private readonly IWishLists wishListManager;

        public WishListController(IWishLists wishListManager) => this.wishListManager = wishListManager;


        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await wishListManager.GetById(id);
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
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] WishListRequest request)
        {
            try
            {
                var result = await wishListManager.Create(request);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        [ValidationModel]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await wishListManager.Delete(id);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}
