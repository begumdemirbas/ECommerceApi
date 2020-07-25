using System;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApi.Data.Data.Core;
using ECommerceApi.Domain.AggregatesModel.ProductAggregate;
using ECommerceApi.Models;

namespace ECommerceApi.Application.Queries
{
    public class ProductQuery: IProductQuery
    {
        private readonly IRepository<Product> _productRepository;
        public ProductQuery(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductDataModel GetById(KeyInputModel inputModel)
        {
            var model = new ProductDataModel();
            var product = _productRepository.GetByKey(inputModel.Id);
            if (product == null)
                throw new ApplicationException("ProductNotFound");
            model = new ProductDataModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Barcode = product.Barcode,
                Price = product.Price,
                Stock = product.Stock,
                Images = product.Images.Select(x => new ProductImageDataModel
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl
                }).ToList()
            };
            return model;
        }

        public ProductListDataModel GetAll()
        {
            var model = new ProductListDataModel();
            var dataCount = _productRepository.GetCount();
            if(dataCount == 0)
            {
                model.Message = "No data found";
            }
            else
            {
                model.DataCount = dataCount;
                model.Data =  _productRepository.GetAll().Select(product => new ProductDataModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Barcode = product.Barcode,
                    Price = product.Price,
                    Stock = product.Stock,
                    Images = product.Images.Select(image => new ProductImageDataModel
                    {
                        Id = image.Id,
                        ImageUrl = image.ImageUrl
                    }).ToList()
                }).ToList();
            }
            return model;
        }
    }
}
