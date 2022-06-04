using DAL.Entity.User;

namespace BLL.Repository
{
    public class UserRepository
    {
        /// <summary>
        /// user repository
        /// </summary>
        /// <value></value>
        public static List<User> User_List { get; set; }

        /// <summary>
        /// creates new user
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>created user</returns>
        public static User CreateUser(string username, string password, string email)
        {
            if (User_List.Where(x => x.Username == username).Any() || User_List.Where(x => x.Email == email).Any())
            {
                throw new Exception($"User already exists");
            }

            var newUser = new User(username, password, email);
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
        /// <param name="userName"></param>
        /// <returns>List<User></returns>
        public static List<User> SearchUser(string userName)
        {
            return User_List.Where(x => x.Username == userName).ToList();
        }

        /// <summary>
        /// returns true if a user with specified properties exists
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>List<User></returns>
        public static bool UserExists(string userName)
        {
            return User_List.Exists(x => x.Username == userName);
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
        public void DeleteUser(int id)
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
