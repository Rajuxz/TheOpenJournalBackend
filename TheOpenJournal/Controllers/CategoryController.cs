using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
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
               bool response = await _categoryServices.CreateCategoryAsync(categoryDto);
                if (!response)
                {
                    return BadRequest(new { message = "Cannot add Category." });
                }

                return Ok(new {message = "Category added Successfully !"});
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
           if(response == null)
            {
                return NotFound(new { message = "Category is empty!" });
            }else
            {
                return Ok(response);
            }
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategories([FromBody] UpdateCategoryDTO categoryDto)
        {
            if (ModelState.IsValid) {
                bool response = await _categoryServices.UpdateCategoryAsync(categoryDto);
                if (!response)
                {
                    return BadRequest(new { message = "Cannot Update !" });
                }

                return Ok(new {message="Updated Successfully"});
            }
            return Ok(new { message = "Updated Successfully." });

        }

        [HttpDelete("delete-category")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Id is empty" });
            }
            bool response = await _categoryServices.DeleteCategoryAsync(id);
            if (!response)
            {
                return BadRequest("Id is empty");
            }
            return Ok(new {message="Deleted Successfully"});
        }
        
    }
}
