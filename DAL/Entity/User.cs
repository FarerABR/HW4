using DAL.Enum.User;

namespace DAL.Entity
{
    public class User
    {
		public User(string username, string password, string email, UserRole role, ushort id)
		{
            Username = username;
            Password = password;
            Email = email;
            Role = role;
            if (role == UserRole.admin) { Id = 0; Balance = -1; }
            else Id = id;
            Date_Created = DateTime.Now;
		}

        public bool IsDeleted { get; set; } = false;

        public bool StayLoggedIn { get; set; } = false;

        public UserRole Role { get; set; }

        public ushort Id;

        public string FirstName { get; set; } = "not set";
        public string LastName { get; set; } = "not set";

        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public UserGender Gender { get; set; } = UserGender.miscellaneous;

        public decimal Balance { get; set; } = 10000;

        public DateTime Date_Created;
    }
}
