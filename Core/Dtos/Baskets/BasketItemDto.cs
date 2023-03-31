using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Baskets
{
    public class BasketItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int BasketId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
