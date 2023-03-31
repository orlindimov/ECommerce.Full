using API.Filters;
using AutoMapper;
using Core.Dtos.ProductDtos;
using Core.RequestParameters;
using Core.Services;
using E.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Stripe;
using System.Drawing.Printing;
using Product = E.Core.Models.Product;

namespace API.Controllers
{
    [Authorize]

    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;

        public ProductsController(IProductService productService, IWebHostEnvironment environment, IFileService fileService)
        {

            _service = productService;
            _environment = environment;
            _fileService = fileService;
        }

        /// GET api/products/GetProductsWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _service.GetProductsWitCategory());
        }

        /// GET api/products
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]Pagination pagination)
        {
          
            return CreateActionResult(await _service.GetAllProducts(pagination));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product, ProductDto>))]
        // GET /api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _service.GetByIdAsync(id));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Save(ProductCreateDto productDto)
        {
            return CreateActionResult(await _service.AddAsync(productDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            return CreateActionResult(await _service.UpdateAsync(productDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product, ProductDto>))]
        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _service.RemoveAsync(id));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            _fileService.UploadAsync("product-images", Request.Form.Files);

            return Ok();
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please select a file.");

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok($"File {fileName} has been uploaded.");
        }


    }
}
