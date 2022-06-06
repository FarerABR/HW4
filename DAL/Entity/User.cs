using DAL.Enum.User;

namespace DAL.Entity.User
{
    public class User
    {
		public User(string username, string password, string email, UserRole role)
		{
            Username = username;
            Password = password;
            Email = email;
            Role = role;
            Balance = 1000;
		}

        public bool IsDeleted { get; set; } = false;

        public bool IsLastLoggedIn { get; set; } = false;

        public UserRole Role { get; set; }

        private int _Id { get; set; }
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = Id_Seed++;
            }
        }

        public string FirstName { get; set; } = "not set";
        public string LastName { get; set; } = "not set";

        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public UserGender Gender { get; set; }

        public decimal Balance { get; set; }

        private static int Id_Seed { get; set; } = 100;
    }
}
