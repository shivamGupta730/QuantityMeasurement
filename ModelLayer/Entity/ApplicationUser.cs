namespace ModelLayer.Entity
{
    public class ApplicationUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}