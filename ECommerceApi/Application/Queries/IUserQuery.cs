using System.Threading.Tasks;

namespace ECommerceApi.Application.Queries
{
    public interface IUserQuery
    {
        Task<bool> CanUserLogin(string userName, string password);
    }
}
