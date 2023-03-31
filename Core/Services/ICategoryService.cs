using Core.Dtos;
using Core.Dtos.ProductDtos;
using Core.Responses;
using E.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICategoryService:IService<Category,CategoryDto>
    {
       Task<CustomResponseDto<CategoryDto>> AddAsync(CreateCategoryDto dto);

       Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId);
    }
}
