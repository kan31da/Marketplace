using Marketplace.Core.Constants;
using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
using Marketplace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    [Authorize]
    [Authorize(Roles = UserConstants.Roles.User)]
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly ICartService cartService;

        public UserController(RoleManager<IdentityRole> _roleManager,
            IUserService _userService,
            ICartService _cartService,
            UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userService = _userService;
            userManager = _userManager;
            cartService = _cartService;
        }
        public IActionResult Index()
        {
            return Redirect("/");
        }

        public async Task<IActionResult> UserDetails()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var user = await userService.GetUsersToEdit(currentUser.Id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UserDetails(string id, UserEditViewModel model)
        {

            if (!ModelState.IsValid || id != model.Id)
            {
                return View(model);
            }

            if (await userService.SelfEditUser(model))
            {
                ViewData[MessageConstant.SuccessMessage] = "Edit Success";
            }
            else
            {
                ViewData[MessageConstant.WarningMessage] = "Invalid Changes";
            }

            return View(model);
        }

        public async Task<IActionResult> UserCart()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var cartDetais = await cartService.GetCartProducts(currentUser.Id);

            return View(cartDetais);
        }

        public async Task<IActionResult> UserOrders()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var orders = await userService.GetOrders(currentUser.Id);

            if (orders == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(orders);
        }

        public async Task<IActionResult> OrdersHistory()
        {

            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var orders = await userService.GetOrdersHistory(currentUser.Id);

            if (orders == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(string orderId)
        {

            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            if (currentUser == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var orders = await userService.GetOrderDetails(currentUser.Id, orderId);

            if (orders == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(orders);
        }
    }
}
