using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Basket:BaseEntity
    {
        public string UserId { get; set; }

       // public Order Orders { get; set; }

       // public UserApp User { get; set; }

        public List<BasketItem> BasketItems { get; set; } 

        public string PaymentIntentId { get; set; }

        public string ClientSecret { get; set; }
    }
}
