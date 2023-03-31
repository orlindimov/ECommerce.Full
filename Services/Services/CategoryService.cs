using AutoMapper;
using Core.Dtos;
using Core.Dtos.ProductDtos;
using Core.Repositories;
using Core.Responses;
using Core.Services;
using Core.UnitOfWorks;
using E.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryService : Service<Category, CategoryDto>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitOfWork, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CustomResponseDto<CategoryDto>> AddAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(category);

            await _unitOfWork.CommitAsync();
            var categoryDto= _mapper.Map<CategoryDto>(category);

            return CustomResponseDto<CategoryDto>.Success(StatusCodes.Status201Created, categoryDto);
        }

        public async Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCategoryByIdWithProductsAsync(categoryId);

            var categoryDto = _mapper.Map<CategoryWithProductsDto>(category);

            return CustomResponseDto<CategoryWithProductsDto>.Success(200, categoryDto);
        }
    }
}
