using System.Linq;
using System.Threading.Tasks;
using ECommerceApi.Application.Queries;
using ECommerceApi.Domain.AggregatesModel.ProductAggregate.Services;
using ECommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> View(KeyInputModel inputModel)
        {
            var product = await _productQuery.GetByIdAsync(inputModel);
            return View(product);
        }

        public async Task<IActionResult> List()
        {
            var products = await _productQuery.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Detail(KeyInputModel inputModel)
        {
            var product = await _productQuery.GetByIdAsync(inputModel);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductInputModel inputModel)
        {
            var productId = await _productService.AddProductAsync(inputModel.Name, inputModel.Description, inputModel.Barcode, inputModel.Price,
                inputModel.Stock, inputModel.Images.Select(x => x.ImageUrl).ToList());

            return RedirectToAction("Detail", productId);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateInputModel inputModel)
        {
            var productId = await _productService.UpdateProductAsync(inputModel.Id, inputModel.Name, inputModel.Description, inputModel.Barcode, inputModel.Price,
                inputModel.Stock, inputModel.Images.Select(x => x.ImageUrl).ToList());
            return RedirectToAction("Detail", productId);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(KeyInputModel inputModel)
        {
            await _productService.DeleteProductAsync(inputModel.Id);
            return RedirectToAction("List");
        }
    }
}
