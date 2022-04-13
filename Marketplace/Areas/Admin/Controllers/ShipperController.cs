using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
using Marketplace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Marketplace.Areas.Admin.Controllers
{
    public class ShipperController : BaseController
    {
        private readonly IShipperService shipperService;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public ShipperController(RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IShipperService _shipperService,
            IUserService _userService)
        {
            userManager = _userManager;
            shipperService = _shipperService;
            userService = _userService;
            roleManager = _roleManager;
        }
        public IActionResult Index()
        {
            return Redirect("/");
        }

        public async Task<IActionResult> Orders()
        {

            var orders = await shipperService.GetOrders();

            if (orders == null)
            {
                return Redirect("/");
            }

            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(string orderId)
        {

            var order = await shipperService.GetOrderDetails(orderId);

            if (order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }


        public async Task<IActionResult> AddShipperToOrder(string orderId)
        {
            if (orderId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var shippers = await shipperService.GetShippers(orderId);

            return View(shippers);
        }

        public async Task<IActionResult> AddOrder(string userId, string orderId)
        {
            //var currentUser = await userManager.GetUserAsync(User);

            //if (currentUser == null)
            //{
            //    return RedirectToAction(nameof(Index));
            //}

            //var ordersToShip = await shipperService.GetOrdersToShip(currentUser.Id);

            return Ok();
        }


        public async Task<IActionResult> RemoveOrder(string orderId)
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (orderId == null)
            {
                return Redirect(nameof(Orders));
            }

            if (await shipperService.RemoveOrder(orderId))
            {
                return Redirect(nameof(Orders));
            }

            return Redirect(nameof(Orders));
        }
    }
}
