using AutoMapper;
using Core.Dtos.Orders;
using Core.Models;
using Core.Repositories;
using Core.Responses;
using Core.Services;
using Core.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OrderService : Service<Order, OrderDto>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<UserApp> _userManager;

        public OrderService(IGenericRepository<Order> repository, IUnitOfWork unitOfWork, IMapper mapper, IOrderRepository orderRepository, UserManager<UserApp> userManager) : base(repository, unitOfWork, mapper)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteAsync(int basketId, string userId)
        {
            var userApp = await _userManager.FindByIdAsync(userId.ToString());

            var order = await _orderRepository.GetByCustomerAndShopAsync(basketId, userApp.OrderId.ToString());

            if (order == null) return CustomResponseDto<NoContentDto>.Fail(404, "error");

            _orderRepository.Remove(order);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(200);
        }

        public async Task<CustomResponseDto<OrderDto>> GetByCustomerAndShopAsync(CreateOrderDto createOrderDto)
        {
            var order = _mapper.Map<OrderDto>(createOrderDto.BasketId);

            var orderDto = await _orderRepository.GetByCustomerAndShopAsync(order.Id, order.UserId);

            if (orderDto == null) return CustomResponseDto<OrderDto>.Fail(400, "error");

            var result = _mapper.Map<OrderDto>(orderDto);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<OrderDto>.Success(200, result);
        }
    }
}
