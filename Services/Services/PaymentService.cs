using AutoMapper;
using Core.Dtos.Baskets;
using Core.Repositories;
using Core.Responses;
using Core.Services;
using Core.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IBasketRepository basketRepository, IMapper mapper, IConfiguration config, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _config = config;
            _productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }



        public async Task<CustomResponseDto<BasketDto>> CreateOrUpdatePaymentIntent(int baskedId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var basket = await _basketRepository.GetByIdAsync(baskedId);

            foreach (var item in basket.BasketItems)
            {
                var product = await _productRepository.GetByIdAsync(item.Id);

                if (item.Price != product.Price)
                    item.Price = product.Price;
            }
            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(basket.BasketItems.Sum(x => x.Quantity * (x.Price * 100))),

                    Currency = "gbp",
                    PaymentMethodTypes = new List<string>() { "card" }

                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;


            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {

                    Amount = (long)basket.BasketItems.Sum(i => i.Quantity * (i.Price * 100))
                };

                await service.UpdateAsync(basket.PaymentIntentId, options);

            }
            _basketRepository.Update(basket);
            var basketDto = _mapper.Map<BasketDto>(basket);
            await unitOfWork.CommitAsync();
            return CustomResponseDto<BasketDto>.Success(200);
        }
    }
}
