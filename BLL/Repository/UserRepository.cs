using DAL.Entity.User;
using DAL.Enum.User;
using BLL.Repository;

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
        /// creates new user
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>created user</returns>
        public static User CreateUser(string username, string password, string email, UserRole role)
        {
            username = username.ToLower();
            email = email.ToLower();
            if (User_List.Where(x => x.Username == username).Any() || User_List.Where(x => x.Email == email).Any())
            {
                throw new Exception($"User already exists");
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
            return User_List.Where(x => x.Username == username).ToList();
        }

        /// <summary>
        /// returns the user with given properties
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List<User></returns>
        public static User SearchUser(string username)
        {
            return User_List.Find(x => x.Username == username);
        }

		/// <summary>
		/// returns true if a user with specified properties exists
		/// </summary>
		/// <param name="username"></param>
		/// <returns>bool</returns>
		public static bool UsernameExists(string username)
        {
			return User_List.Any(x => x.Username == username);
		}
        public static bool UserEmailExists(string email)
        {
            email = email.ToLower();
            return User_List.Any(x => x.Email == email);
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
        public static void DeleteUser(int id)
        {
            if (!User_List.Exists(x => x.Id == id))
            {
                throw new Exception("User doesn't exist");
            }

            var user = User_List.SingleOrDefault(x => x.Id == id);
            if (user.IsDeleted == true)
            {
                throw new Exception("User is already deleted");
            }

            user.IsDeleted = true;
        }
    }
}
