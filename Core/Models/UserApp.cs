using E.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserApp:IdentityUser<string>
    {
        public string City { get; set; }

        public ICollection<Basket> Baskets { get; set; }

        public List<Product> Products { get; set; }

        public List<Order> Orders { get; set; }

        public int OrderId { get; set; }
    }
}
