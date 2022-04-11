using Marketplace.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        public async Task< IActionResult> Index()
        {
            var products = await productService.GetProducts();

            return View(products);
        }

        public async Task<IActionResult> Details(string id)
        {           
            return View();
        }
    }
}
