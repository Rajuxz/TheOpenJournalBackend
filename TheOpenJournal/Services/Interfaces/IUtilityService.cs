namespace TheOpenJournal.Services.Interfaces
{
    public interface IUtilityService
    {
        public Task<string> SaveImage(IFormFile file);
    }
}
