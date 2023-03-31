using Core.Dtos.Orders;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : CustomBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            return CreateActionResult(await _orderService.GetByCustomerAndShopAsync(createOrderDto));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderAsync(int basketId, string userId)
        {
            return CreateActionResult(await _orderService.DeleteAsync(basketId, userId));
        }
    }
}
