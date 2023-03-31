using E.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }
       
       // public int? CategoryId { get; set; }
    }
}
