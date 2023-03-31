using Core.Dtos.Baskets;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : CustomBaseController
    {
        private readonly IBasketService _basketService;
 

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
       
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketAsync(string userId)
        {
            return CreateActionResult(await _basketService.GetBasketAsync(userId));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            return CreateActionResult(await _basketService.SaveOrUpdateAsyn(basketDto));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFromBasketAsync(int Id)
        {
            return CreateActionResult(await _basketService.DeleteFromBasketAsync(Id));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddBasketItemAsync(CreateBasketItemDto createBasketItemDto)
        {
            return CreateActionResult(await _basketService.AddBasketItem(createBasketItemDto));
        }
    }
}
