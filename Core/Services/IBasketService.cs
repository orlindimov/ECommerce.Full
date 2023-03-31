using Core.Dtos.Baskets;
using Core.Models;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IBasketService:IService<Basket,BasketDto>
    {
        Task<CustomResponseDto<BasketDto>> GetBasketAsync(string userId);

        Task<CustomResponseDto<bool>> SaveOrUpdateAsyn(BasketDto basketDto);

        Task<CustomResponseDto<NoContentDto>> DeleteFromBasketAsync(int Id);

        Task<CustomResponseDto<BasketItemDto>> AddBasketItem(CreateBasketItemDto createBasketItemDto);
    }
}
