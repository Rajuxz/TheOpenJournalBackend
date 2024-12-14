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
        public async Task<bool> CreateCategoryAsync(CategoryDTO categoryDto)
        {
            //if category is null or empty
            if (categoryDto == null) {
                return false;
            }
            //map categoryDto to category
            //here Map<Destination>(source)
            var category = _mapper.Map<Category>(categoryDto);

            //check if category already exists:
            var exists = _repository.GetQueryable()
                        .Any(c=>c.Name.ToLower() == categoryDto.Name.ToLower());

            if (exists)
            {
                return false;
            }
            //call repository and add the category
            int response = await _repository.AddAsync(category);

            //if response is 0 or less, it is internal server error.
            if (response <= 0)
            {
                //send false
                return false;
            }
            
                //send success response.
              return true ;
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            //find category
            var category = await _repository.GetByIdAsync(categoryId);
            //if category not found, then just return false.
            if(category == null)
            {
                return false;

            }
            else
            {

                //otherwise, delete data
                int response = await _repository.DeleteAsync(category);
                //if negative response, delete fail else true
                return response < 0 ? false : true;
            }
        }

        public async Task<List<CategoryDTO>> GetCategoriesAsync()
        {
            var categories = _repository.GetQueryable().Select(category => new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            });

            return await categories.ToListAsync();
        }

        public async Task<bool> UpdateCategoryAsync(UpdateCategoryDTO categoryDto)
        {
            //check if category exists or not.
            
            var existingCategory = await _repository.GetByIdAsync(categoryDto.Id);
            //if category exists:
            if(existingCategory != null)
            {
                //change the category
                existingCategory.Name = categoryDto.Name;
                //save the category again !
                int status = await _repository.UpdateAsync(existingCategory);
                if(status <= 0)
                {
                    return false;
                }
                return true;
            }

            return false;
           // var categoryDto = _mapper.Map<CategoryDTO>(categories);
        }

        
    }
}
