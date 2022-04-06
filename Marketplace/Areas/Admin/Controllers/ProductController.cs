using Marketplace.Core.Constants;
using Marketplace.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageProducts()
        {
            var products = await productService.GetProducts();

            return View(products);
        }

        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        //public async Task<IActionResult> AddCategory() //string categoryName
        //{
        //    if (await productService.CreateCategory(CategoryConstants.HAND_TOOL))
        //    {
        //        return Ok();
        //    }

        //    return View();
        //}
    }
}

