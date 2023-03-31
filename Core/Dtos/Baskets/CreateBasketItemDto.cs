using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Baskets
{
    public class CreateBasketItemDto
    {
        public int ProductId { get; set; }

        public int BasketId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public string UserId { get; set; }
    }
}
