namespace TheOpenJournal.Models.Domain
{
    public class AdminModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get;set; }
        public string Password { get; set; }
        public string PhoneNumber { get;set; }
        public string? ProfileUrl { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
