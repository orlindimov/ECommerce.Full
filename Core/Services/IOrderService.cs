using Core.Dtos.Orders;
using Core.Models;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IOrderService:IService<Order,OrderDto>
    {
        Task<CustomResponseDto<NoContentDto>> DeleteAsync(int basketId, string userId);

        Task<CustomResponseDto<OrderDto>> GetByCustomerAndShopAsync(CreateOrderDto createOrderDto);
    }
}
