using Core.Dtos.Baskets;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Orders
{
    public class OrderDto:BaseDto
    {
        public string UserId { get; set; }
        public int BasketId { get; set; }
        public BasketDto BasketDto { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string BuyerEmail { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
