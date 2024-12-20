namespace TheOpenJournal.Services.Interfaces
{
    public interface IUtilityService
    {
        public Task<string> SaveImageAsync(IFormFile file);
    }
}
