
namespace Utility.Authentication
{
    public class PasswordHash
    {
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }
        
        public PasswordHash()
        { }

        public PasswordHash(byte[] salt, byte[] hash)
        {
            Salt = salt;
            Hash = hash;
        }
    }
}
