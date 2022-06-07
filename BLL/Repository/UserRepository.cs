using DAL.Entity.User;
using DAL.Enum.User;

namespace BLL.Repository
{
    public class UserRepository
    {
        /// <summary>
        /// user repository
        /// </summary>
        /// <value></value>
        public static List<User> User_List { get; set; } = new List<User>();

        /// <summary>
        /// creates a new user
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>created user, and returns null if user already exists</returns>
        public static User CreateUser(string username, string password, string email, UserRole role)
        {
            username = username.ToLower();
            email = email.ToLower();
            if (User_List.Where(x => x.Username == username).Any() || User_List.Where(x => x.Email == email).Any())
            {
                throw new Exception("User already exists");
            }

            password = PasswordRepository.HashPassword(password);

            var newUser = new User(username, password, email, role);
            User_List.Add(newUser);
            return newUser;
        }

        /// <summary>
        /// returns the user by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public static User GetUserById(int id)
        {
            return User_List.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// returns all the users with given 
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List<User></returns>
        public static List<User> SearchUsers(string username)
        {
            return User_List.Where(x => x.Username.ToLower() == username.ToLower()).ToList();
        }

        /// <summary>
        /// seraches for any user with given properties
        /// </summary>
        /// <param name="username"></param>
        /// <returns>user if found, and null of none found</returns>
        public static User SearchUser(string username)
        {
            return User_List.Find(x => x.Username.ToLower() == username.ToLower());
        }

        /// <summary>
        /// returns the last user logged in
        /// </summary>
        /// <returns>User</returns>
        /// public static User SearchUser(string username)
        public static User LastLoggedIn()
        {
            return User_List.Find(x => x.IsLastLoggedIn == true);
        }

        /// <summary>
        /// returns true if any user with specified Username exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns>boolean</returns>
        public static bool UsernameExists(string username)
        {
			return User_List.Any(x => x.Username.ToLower() == username.ToLower());
		}

        /// <summary>
		/// returns true if a user with specified Email addess exists
		/// </summary>
		/// <param name="email"></param>
		/// <returns>boolean</returns>
        public static bool UserEmailExists(string email)
        {
            return User_List.Any(x => x.Email.ToLower() == email.ToLower());
        }

        /// <summary>
        /// updates the user
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns>input</returns>
        public static User UpdateUser(User inputUser)
        {
            if (!User_List.Exists(x => x.Id == inputUser.Id))
            {
                throw new Exception("User doesn't exist");
            }
            int userIndex = User_List.FindIndex(x => x.Id == inputUser.Id);
            User_List[userIndex] = inputUser;
            return inputUser;
        }

        /// <summary>
        /// deletes the user from User_List
        /// </summary>
        /// <param name="id"></param>
        public static bool DeleteUser(int id)
        {
            var user = User_List.SingleOrDefault(x => x.Id == id);

            return User_List.Remove(user);
        }
    }
}
