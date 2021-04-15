using System;
using System.Security.Cryptography;
using System.Text;

namespace Utility.Authentication
{
    public static class PasswordUtility
    {
        public static byte[] StringToBytes(string s) => Encoding.UTF8.GetBytes(s);

        public static byte[] GenerateSalt(int bytes)
        {
            if (bytes < 1)
                throw new ArgumentOutOfRangeException("bytes", "Bytes must not be less than 1.");

            byte[] salt = new byte[bytes];
            new RNGCryptoServiceProvider().GetBytes(salt);

            return salt;
        }

        public static PasswordHash GeneratePasswordHash(string password)
            => GeneratePasswordHash(password, GenerateSalt(32));

        public static PasswordHash GeneratePasswordHash(string password, byte[] salt)
            => GeneratePasswordHash(StringToBytes(password), salt);

        public static PasswordHash GeneratePasswordHash(byte[] password, byte[] salt)
        {
            byte[] saltedPassword = new byte[password.Length + salt.Length];

            for (int i = 0; i < password.Length; i++)
                saltedPassword[i] = password[i];
            for (int i = 0; i < salt.Length; i++)
                saltedPassword[password.Length + i] = salt[i];
            
            return new PasswordHash(salt, new SHA256Managed().ComputeHash(saltedPassword));
        }

        public static bool CheckPassword(string password, PasswordHash passwordHash)
            => CheckPassword(StringToBytes(password), passwordHash);

        public static bool CheckPassword(byte[] password, PasswordHash passwordHash)
        {
            PasswordHash generatedSaltedHash = GeneratePasswordHash(password, passwordHash.Salt);
            return CompareByteArrays(generatedSaltedHash.Hash, passwordHash.Hash);
        }

        public static bool CompareByteArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }

            return true;
        }
    }
}
