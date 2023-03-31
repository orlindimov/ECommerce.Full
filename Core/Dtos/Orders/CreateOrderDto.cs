using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Orders
{
    public class CreateOrderDto
    {
        public string UserAppId { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public List<OrderItemDto> OrderItemDto { get; set; }

        public int BasketId { get; set; }
    }
}
