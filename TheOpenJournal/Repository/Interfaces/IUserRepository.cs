using TheOpenJournal.Models.Domain;

namespace TheOpenJournal.Repository.Interfaces
{
    public interface IUserRepository:IRepository<UserModel>
    {
       Task<UserModel> GetEmailAsync(string email);
       Task<string> GetIdByEmailAsync(string email);
    }
}
