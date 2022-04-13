using Marketplace.Core.Constants;
using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
using Marketplace.Core.Utilities;
using Marketplace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    [Authorize]
    [Authorize(Roles = UserConstants.Roles.User)]
    public class CartController : BaseController
    {
        private readonly ICartService cartService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ICartService _cartService, UserManager<ApplicationUser> _userManager)
        {
            cartService = _cartService;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            return Redirect("/");
        }

        public async Task<IActionResult> AddProductToCart(string productId)
        {

            var currentUser = await userManager.GetUserAsync(User);

            if (await cartService.AddProductToCart(currentUser.Id, productId))
            {
                ViewData[MessageConstant.SuccessMessage] = "Тhe product was added successfully";
            }

            return Redirect("/Product");
        }
        public async Task<IActionResult> Edit(string productId)
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (productId == null || currentUser == null)
            {
                return RedirectToAction("UserCart", "User");
            }

            var product = await cartService.GetProductToEdit(currentUser.Id, productId);

            if (product == null)
            {
                return RedirectToAction("UserCart", "User");
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CartProductsViewModel model)
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser == null || model.Id == null)
            {
                return RedirectToAction("UserCart", "User");
            }

            var product = await cartService.GetProductToEdit(currentUser.Id, model.Id);

            if (model.Quantity < GlobalConstants.CartProduct.FIRST_QUANTITY)
            {
                ViewData[MessageConstant.WarningMessage] = "Minimum Quantity is 1";
                return View(product);
            }

            if (await cartService.CartProductToEdit(currentUser.Id, model.Id, model.Quantity))
            {
                return RedirectToAction("UserCart", "User");
            }

            return View(product);
        }

        public async Task<IActionResult> DeleteProductCart(string productId)
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (productId == null || currentUser == null)
            {
                return RedirectToAction("UserCart", "User");
            }

            if (await cartService.DeleteProductCart(currentUser.Id, productId))
            {
                return RedirectToAction("UserCart", "User");
            }

            return RedirectToAction("UserCart", "User");
        }

        public async Task<IActionResult> UserOrder()
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return Redirect("/");
            }

            var order = await cartService.GetUsersCurrentOrder(currentUser.Id);

            if (order == null)
            {
                return Redirect("/");
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> UserOrder(OrderProductViewModel model)
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return Redirect("/");
            }

            var modelOrder = await cartService.GetUsersCurrentOrder(currentUser.Id);

            model.Products = modelOrder.Products;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var order = await cartService.OrderProductCart(currentUser.Id, model.DeliveryAddress);

            return Redirect("/");
        }       
    }
}
