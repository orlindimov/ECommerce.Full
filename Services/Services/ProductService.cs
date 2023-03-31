using AutoMapper;
using Core.Dtos.ProductDtos;
using Core.Hubs;
using Core.Repositories;
using Core.RequestParameters;
using Core.Responses;
using Core.Services;
using Core.UnitOfWorks;
using E.Core.Models;
using Microsoft.AspNetCore.Http;
using Repository.Repositories;
using System.Linq.Expressions;

namespace Service.Services
{
    public class ProductService : Service<Product, ProductDto>, IProductService
    {
        private readonly IProductRepository  _productRepository;
        private readonly IProductHubService _productHubService;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository, IProductHubService productHubService) : base(repository, unitOfWork, mapper)
        {
            _productRepository = productRepository;
            _productHubService = productHubService;
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            await _productRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            var newDto = _mapper.Map<ProductDto>(entity);
           await _productHubService.ProductAddedMessageAsync($"{newDto.Name} Added");
            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status201Created, newDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> Delete(int id)
        {

            var product= await _productRepository.GetByIdAsync(id);
            if(product != null)
            {
                _productRepository.Remove(product);
            }            
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(200);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWitCategory()
        {
            var products = await _productRepository.GetProductsWitCategoryAsync();

            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);

            _productRepository.Update(entity);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<ProductDto>>GetAllProducts(Pagination pagination)
        {
             _productRepository.GetAll().Skip(pagination.Page*pagination.Size).Take(pagination.Size).ToList();

            return CustomResponseDto<ProductDto>.Success(200);


        }
    }
}
