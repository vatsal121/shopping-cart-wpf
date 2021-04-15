
namespace Utility.Authentication
{
    public static class PasswordUtilityCore
    {
        /// <summary>
        /// Hashes the password with a random salt, and returns the salt and hash.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <returns>A PasswordHash object containing the salt and hash.</returns>
        public static PasswordHash GeneratePasswordHash(string password)
            => PasswordUtility.GeneratePasswordHash(password);

        /// <summary>
        /// Checks whether the given password correctly matches the stored password hash.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="passwordHash">The stored password hash and salt.</param>
        /// <returns>True if the password is correct, false if incorrect.</returns>
        public static bool CheckPassword(string password, PasswordHash passwordHash)
            => PasswordUtility.CheckPassword(password, passwordHash);
    }
}
