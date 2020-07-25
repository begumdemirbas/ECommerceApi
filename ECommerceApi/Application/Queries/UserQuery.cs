using System.Threading.Tasks;
using ECommerceApi.Data.Data.Core;
using ECommerceApi.Domain.AggregatesModel.UserAggregate;

namespace ECommerceApi.Application.Queries
{
    public class UserQuery : IUserQuery
    {
        private readonly IRepository<User> _userRepository;
        public UserQuery(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CanUserLogin(string userName, string password)
        {
            var user = await _userRepository.GetByFilterAsync(x => x.UserName == userName && x.Password == password);
            if (user != null)
                return true;

            return false;
        }
    }
}
