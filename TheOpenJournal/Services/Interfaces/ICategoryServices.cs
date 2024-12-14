using Microsoft.AspNetCore.Mvc;
using TheOpenJournal.Models.DTOs;

namespace TheOpenJournal.Services.Interfaces
{
    public interface ICategoryServices
    {
        public Task<bool> CreateCategoryAsync(CategoryDTO categoryDto);
        public Task<bool> UpdateCategoryAsync(UpdateCategoryDTO categoryDto);
        public Task<bool> DeleteCategoryAsync(Guid categoryId);
        public Task<List<CategoryDTO>> GetCategoriesAsync();
    }
}
