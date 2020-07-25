using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.AggregatesModel.ProductAggregate.Services
{
    public interface IProductService
    {
        /// <summary>
        /// add new product
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="barcode"></param>
        /// <param name="price"></param>
        /// <param name="stock"></param>
        /// <param name="imageUrls"></param>
        /// <returns></returns>
        Task<long> AddProductAsync(string name, string description, string barcode, decimal price, int stock, List<string> imageUrls);

        /// <summary>
        /// update existing product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="barcode"></param>
        /// <param name="price"></param>
        /// <param name="stock"></param>
        /// <param name="imageUrls"></param>
        /// <returns></returns>
        Task<long> UpdateProductAsync(long id,string name, string description, string barcode, decimal price, int stock, List<string> imageUrls);

        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteProductAsync(long id);
    }
}
