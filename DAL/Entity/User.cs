using DAL.Enum.User;

namespace DAL.Entity.User
{
    public class User
    {
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

        public string First_Name { get; set; }
        public string last_Name { get; set; }
        public Gender Gender { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Date_Of_Account { get; set; }
        public string Email { get; set; }


        public decimal Balance { get; set; }

        public static int Id_Seed { get; set; } = 0;


    }
}
