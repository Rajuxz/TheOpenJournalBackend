using TheOpenJournal.Data;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace TheOpenJournal.Repository.Implementations
{
    public class UserRepository:Repository<UserModel>,IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }

        public  async Task<UserModel> GetEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
