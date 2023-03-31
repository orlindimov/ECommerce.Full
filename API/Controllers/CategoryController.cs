using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : CustomBaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateCategoryDto categoryDto)
        {
            return CreateActionResult(await categoryService.AddAsync(categoryDto));
        }
    }
}
