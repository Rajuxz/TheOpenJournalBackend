using AutoMapper;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Models.DTOs;

namespace TheOpenJournal.Mapper
{
    public class CategoryMapper:Profile
    {
        public CategoryMapper()
        {
            //from category to categoryDTo
            CreateMap<Category, CategoryDTO>();

            //from CategoryDto to category
            CreateMap<CategoryDTO, Category>();

        }
    }
}
