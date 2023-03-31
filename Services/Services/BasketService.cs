using AutoMapper;
using Core.Dtos.Baskets;
using Core.Models;
using Core.Repositories;
using Core.Responses;
using Core.Services;
using Core.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BasketService : Service<Basket, BasketDto>, IBasketService
    {

        private readonly IBasketRepository _basketRepository;
        private readonly UserManager<UserApp> _userManager;
        private readonly IBasketItemRepository _basketItemRepository;

        public BasketService(IGenericRepository<Basket> repository, IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository, UserManager<UserApp> userManager, IBasketItemRepository basketItemRepository) : base(repository, unitOfWork, mapper)
        {
            _basketRepository = basketRepository;
            _userManager = userManager;
            _basketItemRepository = basketItemRepository;
        }

        public async Task<CustomResponseDto<BasketItemDto>> AddBasketItem(CreateBasketItemDto createBasketItemDto)
        {
            var basketDto = _mapper.Map<BasketDto>(createBasketItemDto);
            var basket = await _basketRepository.GetBasket(basketDto.UserId);

            var existBasket = _mapper.Map<BasketDto>(basket);

            if (existBasket != null)
            {
                basketDto.BasketItemDtos.Add(new BasketItemDto
                {
                    ProductId = createBasketItemDto.ProductId,
                    Quantity = createBasketItemDto.Quantity,
                    BasketId = createBasketItemDto.BasketId
                });
            }
            await _basketRepository.AddAsync(basket);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<BasketItemDto>.Success(200);
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteFromBasketAsync(int Id)
        {

            var exitstBasket = await _basketItemRepository.GetByIdAsync(Id);

            if (exitstBasket != null)
            {
                _basketRepository.DeleteFromBasket(exitstBasket.Id);
            }

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(200);


        }

        public async Task<CustomResponseDto<BasketDto>> GetBasketAsync(string userId)
        {
            var exitstBasket = await _basketRepository.GetBasket(userId);
            var entity = _mapper.Map<BasketDto>(exitstBasket);
            if (entity == null)
            {
                return CustomResponseDto<BasketDto>.Fail(404, "basket not found");
            }

            return CustomResponseDto<BasketDto>.Success(200, entity);
        }

        public async Task<CustomResponseDto<bool>> SaveOrUpdateAsyn(BasketDto basketDto)
        {
            var basket = _mapper.Map<Basket>(basketDto);

            await _basketRepository.SaveOrUpdate(basket);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<bool>.Success(204);
        }
    }
}
