using System.ComponentModel.DataAnnotations;

namespace TheOpenJournal.Models.DTOs
{
    public class CategoryDTO
    {  
        public Guid? Id { get; set; } 
        public string Name { get; set; }
    }
    public class UpdateCategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
