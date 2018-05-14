using System;
using System.Text;

namespace DateMate.Common.Security
{
	// Salting and hashing.
	public static class Encryption
	{
		// RNG'd salt.
		public static string GenerateSalt()
		{
			var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
			var salt = new byte[8];
			rng.GetBytes(salt);
			return Convert.ToBase64String(salt);
		}

		// Returns a SHA256 hex hash of the salted password.
		public static string GeneratePasswordHash(string password, string salt)
		{
			byte[] encodedInput = Encoding.UTF8.GetBytes(password + salt);
			var hashStr = new System.Security.Cryptography.SHA256Managed();
			byte[] hash = hashStr.ComputeHash(encodedInput);

			return ByteArrayToHexString(hash);
		}

		private static string ByteArrayToHexString(byte[] ba)
		{
			StringBuilder hex = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
				hex.AppendFormat("{0:x2}", b);
			return hex.ToString();
		}
	}
}
