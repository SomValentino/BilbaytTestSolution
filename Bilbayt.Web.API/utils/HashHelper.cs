using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.utils
{
    public static class HashHelper
    {
        public static string Hash(this string password, byte[] salt = null)
        {
            if(salt == null)
               salt = password.GenerateSalt();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt,1000);
            byte[] hash = pbkdf2.GetBytes(20);

            var hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, salt.Length);
            Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool compare(this string password, string verifyPassword)
        {
            var hashbytes = Convert.FromBase64String(password);

            var salt = new byte[16];

            Array.Copy(hashbytes, 0, salt, 0, salt.Length);

            var hash = verifyPassword.Hash(salt);

            return password == hash;
        }

        private static byte[] GenerateSalt(this string password)
        {
            var salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            return salt;
        }

    }
}
