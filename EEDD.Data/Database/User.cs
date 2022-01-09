namespace ServerData.Database
{
    [Table("Users")]
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(64)]
        public string? Token { get; set; }

        [Precision(0)]
        public DateTime? TokenIssued { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.User;

        [Required]
        public bool IsBanned { get; set; } = false;

        public virtual List<Route> Routes { get; set; } = new List<Route>();

        public virtual List<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
