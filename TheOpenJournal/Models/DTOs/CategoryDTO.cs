using System.ComponentModel.DataAnnotations;

namespace TheOpenJournal.Models.DTOs
{
    public class CategoryDTO
    {  
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
