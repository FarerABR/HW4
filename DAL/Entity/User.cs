using DAL.Enum.User;

namespace DAL.Entity
{
    public class User
    {
		public User(string username, string password, string email, UserRole role)
		{
            Username = username;
            Password = password;
            Email = email;
            Role = role;
            if(role == UserRole.admin) { _id = 0; }
            Date_Created = DateTime.Now;
		}

        public bool IsDeleted { get; set; } = false;

        public bool IsLastLoggedIn { get; set; } = false;

        public UserRole Role { get; set; }

        private readonly int _id = Id_Seed++;
        public int Id { get { return _id; } }

        public string FirstName { get; set; } = "not set";
        public string LastName { get; set; } = "not set";

        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public UserGender Gender { get; set; } = UserGender.miscellaneous;

        public decimal Balance { get; set; } = 10000;

        public readonly DateTime Date_Created;

        private static int Id_Seed { get; set; } = 100;
    }
}
