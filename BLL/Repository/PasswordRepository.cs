﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BLL.Repository;

namespace BLL.Repository
{
	public class PasswordRepository
	{
		public static string HashPassword(string password)
		{
			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);

			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);

			string savedPasswordHash = Convert.ToBase64String(hashBytes);
			return savedPasswordHash;
		}

		public static bool CheckPassword(string InputUsername, string InputPassword)
		{
			/* Fetch the stored value */
			string savedPasswordHash = UserRepository.SearchUser(InputUsername).Password;
			/* Extract the bytes */
			byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
			/* Get the salt */
			byte[] salt = new byte[16];
			Array.Copy(hashBytes, 0, salt, 0, 16);
			/* Compute the hash on the password the user entered */
			var pbkdf2 = new Rfc2898DeriveBytes(InputPassword, salt, 10000);
			byte[] hash = pbkdf2.GetBytes(20);
			/* Compare the results */
			for (int i = 0; i < 20; i++)
			{
				if (hashBytes[i + 16] != hash[i])
					return false;
			}
			return true;
		}
	}
}
