using E.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BasketItem:BaseEntity
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
      
        public string Type { get; set; }
        public Basket Basket { get; set; }
        public Product Product { get; set; }

      
    }
}
