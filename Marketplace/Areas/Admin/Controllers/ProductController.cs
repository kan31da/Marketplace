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

        public async Task<IActionResult> EditImages(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(ManageProducts));
            }

            var product = await productService.GetProductToEdit(id);

            if (product == null)
            {
                return RedirectToAction(nameof(ManageProducts));
            }

            return View(product);
        }


        public async Task<IActionResult> Edit(string id)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, ProductToEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (true)
            {
                ViewData[MessageConstant.SuccessMessage] = "Edit Success";
            }
            else
            {
                ViewData[MessageConstant.WarningMessage] = "Invalid Changes";
            }

            return View(model);
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

        [HttpPost]
        public async Task<IActionResult> AddImage(ProductToEditViewModel model)
        {
            return View();
        }

        public async Task<IActionResult> DeleteImage(string id, string imageToDelete)
        {
            return View();
        }
    }
}