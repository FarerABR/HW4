using DAL.Entity.User;

namespace BLL.Repository
{
    public class UserRepository
    {
        /// <summary>
        /// user repository
        /// </summary>
        /// <value></value>
        public List<User> User_List { get; set; }

        /// <summary>
        /// creates new user
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns>input</returns>
        public User CreateUser(User newUser)
        {
            if (User_List.Where(x => x.Username == newUser.Username).Any() || User_List.Where(x => x.Email == newUser.Email).Any())
            {
                throw new Exception($"User already exist");
            }

            User_List.Add(newUser);
            return newUser;
        }

        /// <summary>
        /// returns the user by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public User GetUserById(int id)
        {
            return User_List.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// returns all the users with given 
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns>List<User></returns>
        public List<User> SearchUser(User inputUser)
        {
            return User_List.Where(x => x == inputUser).ToList();
        }

        /// <summary>
        /// updates the user
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns>input</returns>
        public User UpdateUser(User inputUser)
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
