using Core.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class CategoryWithProductsDto:CategoryDto
    {
        public List<ProductDto> Products { get; set; }
    }
}
