using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Baskets
{
    public class BasketDto:BaseDto
    {
        public string UserId { get; set; }

        public string ProductName { get; set; }

        public IList<BasketItemDto> BasketItemDtos { get; set; }

        public string ClientSecret { get; set; }

        public string PaymentIntentId { get; set; }
    }
}
