using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApi.Data.Data.Core;
using ECommerceApi.Domain.AggregatesModel.ProductAggregate;
using ECommerceApi.Domain.AggregatesModel.ProductAggregate.Services;

namespace ECommerceApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<long> AddProductAsync(string name, string description, string barcode, decimal price, int stock, List<string> imageUrls)
        {
            var images = imageUrls.Select(x => new ProductImage(x)).ToList();
            var newProduct = new Product(name, description, barcode, price, stock, images);
            await _productRepository.AddAndSaveAsync(newProduct);

            return newProduct.Id;
        }

        public async Task<long> UpdateProductAsync(long id,string name, string description, string barcode, decimal price, int stock, List<string> imageUrls)
        {
            var product = await _productRepository.GetByKeyAsync(id);
            var images = imageUrls.Select(x => new ProductImage(x)).ToList();

            product.Update(name, description, barcode, price, stock, images);
            await _productRepository.ModifyAndSaveAsync(product);

            return product.Id;
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            var product = await _productRepository.GetByKeyAsync(id);

            product.Delete();
            await _productRepository.ModifyAndSaveAsync(product);

            return true;
        }
    }
}
