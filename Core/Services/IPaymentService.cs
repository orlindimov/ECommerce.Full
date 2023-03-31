using Core.Dtos.Baskets;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPaymentService
    {
        Task<CustomResponseDto<BasketDto>> CreateOrUpdatePaymentIntent(int basketId);
    }
}
