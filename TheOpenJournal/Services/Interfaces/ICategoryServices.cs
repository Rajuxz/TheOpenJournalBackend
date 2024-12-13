using Microsoft.AspNetCore.Mvc;
using TheOpenJournal.Models.DTOs;

namespace TheOpenJournal.Services.Interfaces
{
    public interface ICategoryServices
    {
        public Task<ActionResult> CreateCategoryAsync(CategoryDTO categoryDto);
        public Task<ActionResult> UpdateCategoryAsync(Guid categoryId,CategoryDTO categoryDto);
        public Task<ActionResult> DeleteCategoryAsync(Guid categoryId);
        public Task<ActionResult<IQueryable<CategoryDTO>>> GetCategoriesAsync();
    }
}
