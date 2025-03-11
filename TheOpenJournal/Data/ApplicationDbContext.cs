using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheOpenJournal.Models.Domain;

namespace TheOpenJournal.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // as [Index] annotation was introduced in .Net 7
            // Use fluent api to enforce that Slug is unique.
            modelBuilder.Entity<Post>().HasIndex(p=>p.Slug).IsUnique();
            var hasher = new PasswordHasher<AdminModel>();
            modelBuilder.Entity<AdminModel>().HasData(
                new AdminModel()
                {
                    Id = Guid.NewGuid(),
                    Username = "Admin",
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                    Password = hasher.HashPassword(null, "12345"),
                    PhoneNumber = "9814964044",
                    IsActive = true,
                },
                new AdminModel()
                {
                    Id = Guid.NewGuid(),
                    Username = "Rajuxz",
                    FullName = "Raju",
                    Email = "raju@gmail.com",
                    Password = hasher.HashPassword(null,"R@ju_1"),
                    PhoneNumber = "9745868539",
                    IsActive = true,
                }
             );
        }
        public DbSet<AdminModel> Admin { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
