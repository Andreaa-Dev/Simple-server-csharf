namespace ECommerceAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; } // Note: In a real application, passwords should be hashed
        public required string Email { get; set; }
    }
}

