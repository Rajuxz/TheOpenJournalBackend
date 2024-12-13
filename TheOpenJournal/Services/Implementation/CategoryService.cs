using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Models.DTOs;
using TheOpenJournal.Repository.Interfaces;
using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Services.Implementation
{
    public class CategoryService : ICategoryServices
    {
        private ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository,IMapper mapper) { 
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ActionResult> CreateCategoryAsync(CategoryDTO categoryDto)
        {
            //if category is null or empty
            if (categoryDto == null) {
                return new BadRequestObjectResult("Category name is null");
            }
            //map categoryDto to category
            //here Map<Destination>(source)
            var category = _mapper.Map<Category>(categoryDto);
            //call repository and add the category
            int response = await _repository.AddAsync(category);

            //if response is 0 or less, it is internal server error.
            if (response <= 0)
            {
                //send internal server error
                return new StatusCodeResult(500);
            }
            
                //send success response.
              return new OkResult();
        }

        public Task<ActionResult> DeleteCategoryAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<IQueryable<CategoryDTO>>> GetCategoriesAsync()
        {

            var query = _repository.GetQueryable();
            var categoryDto = query.Select(category => new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            });
            var result = await categoryDto.ToListAsync();
            return new OkObjectResult(result);
        }

        public async Task<ActionResult> UpdateCategoryAsync(Guid categoryId, CategoryDTO categoryDto)
        {
            //check if category exists or not.
            var existingCategory = await _repository.GetByIdAsync(categoryId);
            //if category exists:
            if(existingCategory != null)
            {
                //change the category
                existingCategory.Name = categoryDto.Name;
                //save the category again !
                await _repository.UpdateAsync(existingCategory);
                int status = await _repository.SaveAsync();
                if(status <= 0)
                {
                    return new StatusCodeResult(500);
                }
                return new OkResult();
            }

            return new BadRequestObjectResult("No Category Found");
           // var categoryDto = _mapper.Map<CategoryDTO>(categories);
        }

        
    }
}
