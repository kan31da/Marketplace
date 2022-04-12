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

        public async Task<IActionResult> ManageProductsWithZeroQuantity()
        {
            var products = await productService.GetProductsWithZeroQuantity();

            return View(products);
        }

        public async Task<IActionResult> EditImages(string id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(ManageProducts));
            }

            var product = await productService.GetProductToEditImages(id);

            if (product == null)
            {
                return RedirectToAction(nameof(ManageProducts));
            }

            return View(product);
        }


        public async Task<IActionResult> Edit(string id)
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


        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await productService.ProductToEdit(model))
            {
                ViewData[MessageConstant.SuccessMessage] = "Edit Success";
            }
            else
            {
                ViewData[MessageConstant.WarningMessage] = "Invalid Changes";
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            if (await productService.DeleteProduct(id))
            {
                return RedirectToAction(nameof(ManageProducts)
                    , ViewData[MessageConstant.SuccessMessage] = "Delete Success");
            }
            else
            {
                return RedirectToAction(nameof(ManageProducts)
                    , ViewData[MessageConstant.WarningMessage] = "Invalid Delete");
            }
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
        public async Task<IActionResult> AddImage(ProductToEditImagesViewModel model)
        {
            if (model.Id == null)
            {
                return RedirectToAction(nameof(ManageProducts));
            }

            if (model.Name == null)
            {
                return RedirectToAction(nameof(EditImages), "Product", new { model.Id });
            }

            await productService.AddImage(model.Id, model.Name);

            return RedirectToAction(nameof(EditImages), "Product", new { model.Id });
        }


        public async Task<IActionResult> DeleteImage(string id, string imageToDelete)
        {
            if (id == null || imageToDelete == null)
            {
                return RedirectToAction(nameof(ManageProducts));
            }

            await productService.DeleteImage(id, imageToDelete);

            return RedirectToAction(nameof(EditImages), "Product", new { id });
        }
    }
}