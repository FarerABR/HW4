using DAL.Entity;
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
        /// <returns>created user</returns>
        public static User CreateUser(string username, string password, string email, UserRole role)
        {
            if (User_List.Where(x => x.Username.ToLower() == username.ToLower()).Any() || User_List.Where(x => x.Email.ToLower() == email.ToLower()).Any())
            {
                throw new Exception("User already exists");
            }

            password = PasswordRepository.HashPassword(password);

            User newUser = new(username, password, email, role, GenerateId());
            User_List.Add(newUser);
            return newUser;
        }

        /// <summary>
        /// generates a unique id for the user
        /// </summary>
        /// <returns>a ushort id</returns>
        public static ushort GenerateId()
		{
            ushort ResultId;
            Random t = new();
            do
            {
                ResultId = (ushort)t.Next(65535);
			} while (User_List.Where(x => x.Id == ResultId).Any());
            return ResultId;
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
        public static User StayLoggedInUser()
        {
            if(User_List.Find(x => x.StayLoggedIn == true) != default)
                return User_List.Find(x => x.StayLoggedIn == true);
            return null;
        }

        public static bool IsUserDeleted(string username)
		{
            return User_List.Any(x => x.Username.ToLower() == username.ToLower() && x.IsDeleted);
        }

        public static void ClearStayLoggedIn()
        {
            while(User_List.Find(x => x.StayLoggedIn == true) != default)
			{
                User_List.Find(x => x.StayLoggedIn == true).StayLoggedIn = false;
            }
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

            if (user != null)
            {
                user.IsDeleted = true;
                return true;
            }
            return false;
        }
    }
}
