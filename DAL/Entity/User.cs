using DAL.Enum.User;

namespace DAL.Entity.User
{
    public class User
    {
		public User(string username, string password, string email)
		{
            Username = username;
            Password = password;
            Email = email;
		}

        public bool IsDeleted { get; set; } = false;
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

        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public decimal Balance { get; set; }

        public static int Id_Seed { get; set; } = 100;
    }
}
