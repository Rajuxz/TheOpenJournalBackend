using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheOpenJournal.Models.DTOs;
using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Controllers
{
    [ApiController]
    [Route("api/v1/category")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        internal readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        { 
            _categoryServices = categoryServices;
        }
        [HttpPost("create-categories")]
        public  async Task<IActionResult> CreateCategories(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
               var response = await _categoryServices.CreateCategoryAsync(categoryDto);
               return response;
            }
            else
            {
                return BadRequest(new {message="Model is not valid."});
            }
        }
        [HttpGet("get-categories")]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _categoryServices.GetCategoriesAsync();
            return Ok(response);
        }
        
    }
}
