using DAL.Entity;
using DAL.Entity.Product;
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
        /// Updates password for a specified user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword"></param>
        public static void UpdatePassword(User user, string newPassword)
		{
            newPassword = PasswordRepository.HashPassword(newPassword);
            user.Password = newPassword;
            UpdateUser(user);
        }

        /// <summary>
        /// returns the user by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public static User GetUserById(ushort id)
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
        public static User StayLoggedInUser()
        {
            if(User_List.Find(x => x.StayLoggedIn == true) != default)
                return User_List.Find(x => x.StayLoggedIn == true);
            return null;
        }

        /// <summary>
        /// true if User exists and is deleted, otherwise false
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool IsUserDeleted(string username)
		{
            return User_List.Any(x => x.Username.ToLower() == username.ToLower() && x.IsDeleted);
        }

        /// <summary>
        /// Clears the stay logged in state for any possible user
        /// </summary>
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
        /// tries to promote a user to its next higher available role
        /// </summary>
        /// <param name="fromWho"></param>
        /// <param name="onWho"></param>
        /// <returns>true if promotion done, false if admin being added</returns>
        /// <exception cref="Exception"></exception>
        public static bool PromoteUser(User fromWho, User onWho)
		{
            if (fromWho == null || onWho == null)
                throw new Exception("User cannot be null!");

            if (fromWho.Id == onWho.Id)
            {
                throw new Exception("You cannot promote yourself!");
            }

            if (onWho.Role == UserRole.admin)
			{
                throw new Exception("You cannot promote an admin!");
            }

            if (fromWho.Role == UserRole.admin)
			{
                if (onWho.Role == UserRole.moderator)
                {
                    return false;
				}

                onWho.Role = UserRole.moderator;
            }

            else if(fromWho.Role == UserRole.moderator)
			{
                if(onWho.Role == UserRole.moderator)
				{
                    throw new Exception("You cannot promote a moderator!");
                }

                throw new Exception("You cannot add a moderator!");
            }

            return true;
		}

        /// <summary>
        /// tries to demote a user to its next lower available role
        /// </summary>
        /// <param name="fromWho"></param>
        /// <param name="onWho"></param>
        /// <exception cref="Exception"></exception>
        public static void DemoteUser(User fromWho, User onWho)
        {
            if (fromWho == null || onWho == null)
                throw new Exception("User cannot be null!");

            if (fromWho.Id == onWho.Id)
                throw new Exception("You cannot demote yourself!");

            if (onWho.Role == UserRole.customer)
                throw new Exception("You cannot demote a customer!");

            if (fromWho.Role == UserRole.admin)
            {
                onWho.Role = UserRole.customer;
            }

            else if (fromWho.Role == UserRole.moderator)
            {
                if (onWho.Role == UserRole.admin)
                    throw new Exception("You cannot demote an admin!");

                throw new Exception("You cannot demote a moderator!");
            }
        }

        /// <summary>
        /// transfers adminship to another user
        /// </summary>
        /// <param name="currAdmin"></param>
        /// <param name="newAdmin"></param>
        /// <exception cref="Exception"></exception>
        public static void TransferAdmin(User currAdmin, User newAdmin)
        {
            currAdmin.Role = UserRole.moderator;
            currAdmin.Balance = 10000;
            ushort t = GenerateId();

            List<Product> Cart_List = new();
            foreach(var x in ProductsRepository.Processor_List)
                if(ProductsRepository.IsAddedToCart(x, currAdmin))
                    Cart_List.Add(x);
            foreach (var x in ProductsRepository.GraphicsCard_List)
                if (ProductsRepository.IsAddedToCart(x, currAdmin))
                    Cart_List.Add(x);
            foreach (var x in ProductsRepository.Ram_List)
                if (ProductsRepository.IsAddedToCart(x, currAdmin))
                    Cart_List.Add(x);
            foreach (var x in ProductsRepository.Motherboard_List)
                if (ProductsRepository.IsAddedToCart(x, currAdmin))
                    Cart_List.Add(x);

            foreach(var x in Cart_List)
			{
                int Index = x.AddedToCartIds_List.FindIndex(y => y == currAdmin.Id);
                x.AddedToCartIds_List[Index] = t;
			}
            currAdmin.Id = t;

            newAdmin.Role = UserRole.admin;
            newAdmin.Balance = -1;

            Cart_List.Clear();
            foreach (var x in ProductsRepository.Processor_List)
                if (ProductsRepository.IsAddedToCart(x, newAdmin))
                    Cart_List.Add(x);
            foreach (var x in ProductsRepository.GraphicsCard_List)
                if (ProductsRepository.IsAddedToCart(x, newAdmin))
                    Cart_List.Add(x);
            foreach (var x in ProductsRepository.Ram_List)
                if (ProductsRepository.IsAddedToCart(x, newAdmin))
                    Cart_List.Add(x);
            foreach (var x in ProductsRepository.Motherboard_List)
                if (ProductsRepository.IsAddedToCart(x, newAdmin))
                    Cart_List.Add(x);

            foreach (var x in Cart_List)
            {
                int Index = x.AddedToCartIds_List.FindIndex(y => y == newAdmin.Id);
                x.AddedToCartIds_List[Index] = 0;
            }
            newAdmin.Id = 0;

            ClearStayLoggedIn();
        }

        /// <summary>
        /// deletes the user from User_List, true if successful
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true if user deleted and false when self delete</returns>
        public static bool DeleteUser(User fromWho, ushort id)
        {
            if (fromWho.Id == id)
            {
                if (fromWho.Role == UserRole.admin)
                    throw new Exception("Please transfer admin role to another user\nbefore deleting your account!");
                return false;
            }

            var onWho = User_List.SingleOrDefault(x => x.Id == id);
            if (fromWho.Role == UserRole.moderator)
                if (onWho.Role != UserRole.customer)
                    throw new Exception("You don't have required privileges to do that!");

            onWho.IsDeleted = true;
            return true;
        }

        /// <summary>
        /// self deletes a user account
        /// </summary>
        /// <param name="user"></param>
        public static void SelfDelete(User user)
		{
            user.IsDeleted = true;
		}

        /// <summary>
        /// restores the user to User_List
        /// </summary>
        /// <param name="id"></param>
        /// true if successful and false if user is not deleted
        public static bool RestoreUser(User fromWho, ushort id)
        {
            var onWho = User_List.SingleOrDefault(x => x.Id == id);
            if (onWho.IsDeleted == false)
                return false;

            if (fromWho.Role == UserRole.moderator)
                if (onWho.Role != UserRole.customer)
                    throw new Exception("You don't have required privileges to do that!");

            onWho.IsDeleted = false;
            return true;
        }
    }
}
