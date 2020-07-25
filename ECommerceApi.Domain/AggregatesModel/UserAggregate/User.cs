using ECommerceApi.DomainCore;

namespace ECommerceApi.Domain.AggregatesModel.UserAggregate
{
    public class User : AggregateRoot
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public bool IsAdmin { get; private set; }
        public User()
        {
        }

        public User(string userName, string password, bool isAdmin)
        {
            UserName = userName;
            Password = password;
            IsAdmin = isAdmin;
        }
    }
}
