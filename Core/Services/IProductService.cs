
using Core.Dtos.ProductDtos;
using Core.RequestParameters;
using Core.Responses;
using E.Core.Models;

namespace Core.Services
{
    public interface IProductService:IService<Product ,ProductDto>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWitCategory();

        Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto);

        Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto dto);

        Task<CustomResponseDto<NoContentDto>> Delete(int id);

        Task<CustomResponseDto<ProductDto>> GetAllProducts(Pagination pagination);

    }
}
