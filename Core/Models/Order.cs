using E.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Order:BaseEntity
    {
       // public List<Product> Products { get; set; }
     
        public string Description { get; set; }

        public string Adress { get; set; }

        public int BasketId { get; set; }

        public string UserId { get; set; }

        // public string OrderCode { get; set; }

        public string PaymentIntentId { get; set; }

        public string PaymentToken { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public decimal Total { get; set; }
    }
}
