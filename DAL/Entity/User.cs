using DAL.Enum.User;

namespace DAL.Entity
{
    public class User
    {
		public User(string username, string password, string email, UserRole role, long id)
		{
            Username = username;
            Password = password;
            Email = email;
            Role = role;
            if (role == UserRole.admin) { _id = 0; Balance = -1; }
            else _id = id;
            Date_Created = DateTime.Now;
		}

        public bool IsDeleted { get; set; } = false;

        public bool IsLastLoggedIn { get; set; } = false;

        public UserRole Role { get; set; }

        private readonly long _id;
        public long Id { get { return _id; } }

        public string FirstName { get; set; } = "not set";
        public string LastName { get; set; } = "not set";

        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public UserGender Gender { get; set; } = UserGender.miscellaneous;

        public decimal Balance { get; set; } = 10000;

        public readonly DateTime Date_Created;
    }
}
