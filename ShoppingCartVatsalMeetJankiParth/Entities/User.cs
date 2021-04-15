using Utility.Authentication;

namespace ShoppingCartVatsalMeetJankiParth.Entities
{
    public class User : Entity
    {
        public string Username { get; set; }
        public PasswordHash Password { get; set; }
        public UserRole Role { get; set; }


    }
}
