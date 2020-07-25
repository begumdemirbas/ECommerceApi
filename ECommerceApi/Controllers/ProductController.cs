using ECommerceApi.Application.Queries;
using ECommerceApi.Domain.AggregatesModel.ProductAggregate.Services;
using ECommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApi.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductQuery _productQuery;

        public ProductController(IProductService productService, IProductQuery productQuery)
        {
            _productService = productService;
            _productQuery = productQuery;
        }

        [AllowAnonymous]
        public IActionResult Index(KeyInputModel inputModel)
        {
            var product = _productQuery.GetById(inputModel);
            return View(product);
        }

        public IActionResult List()
        {
            var products = _productQuery.GetAll();
            return View(products);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductInputModel inputModel)
        {
            var productId = await _productService.AddProductAsync(inputModel.Name, inputModel.Description, inputModel.Barcode, inputModel.Price,
                inputModel.Stock, inputModel.Images.Select(x => x.ImageUrl).ToList());
            return RedirectToAction("UpdateProduct", productId);
        }

        public IActionResult UpdateProduct(KeyInputModel inputModel)
        {
            var product = _productQuery.GetById(inputModel);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductUpdateInputModel inputModel)
        {
            var productId = await _productService.UpdateProductAsync(inputModel.Id, inputModel.Name, inputModel.Description, inputModel.Barcode, inputModel.Price,
                inputModel.Stock, inputModel.Images.Select(x => x.ImageUrl).ToList());
            return RedirectToAction("UpdateProduct", productId);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(KeyInputModel inputModel)
        {
            await _productService.DeleteProductAsync(inputModel.Id);
            return RedirectToAction("List");
        }
    }
}