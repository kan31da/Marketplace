using Marketplace.Core.Constants;
using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
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

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await productService.AddProduct(model))
            {
                ViewData[MessageConstant.SuccessMessage] = "Тhe product was added successfully";
            }
            else
            {
                ViewData[MessageConstant.WarningMessage] = "Invalid Parametars";
            }

            return View(model);
        }
    }
}

