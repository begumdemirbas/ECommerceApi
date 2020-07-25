using System.Threading.Tasks;
using ECommerceApi.Models;

namespace ECommerceApi.Application.Queries
{
    public interface IProductQuery
    {
        /// <summary>
        /// gets product data by id
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        ProductDataModel GetById(KeyInputModel inputModel);

        /// <summary>
        /// gets all products
        /// </summary>
        /// <returns></returns>
        ProductListDataModel GetAll();
    }
}
