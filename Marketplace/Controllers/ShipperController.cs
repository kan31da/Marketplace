using Marketplace.Core.Constants;
using Marketplace.Core.Contracts;
using Marketplace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    [Authorize]
    [Authorize(Roles = UserConstants.Roles.Shipper)]
    public class ShipperController : BaseController
    {
        private readonly IShipperService shipperService;
        private readonly UserManager<ApplicationUser> userManager;
        public ShipperController(UserManager<ApplicationUser> _userManager, IShipperService _shipperService)
        {
            userManager = _userManager;
            shipperService = _shipperService;
        }

        public IActionResult Index()
        {
            return Redirect("/");
        }

        public async Task<IActionResult> ShipperOrders()
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var ordersToShip = await shipperService.GetOrdersToShip(currentUser.Id);

            return View(ordersToShip);
        }

        [HttpPost]
        public async Task<IActionResult> ShipperOrders(string orderId)
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser == null || orderId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (await shipperService.FinishOrder(orderId))
            {
                ViewData[MessageConstant.SuccessMessage] = "Order Finished";
            }
            else
            {
                ViewData[MessageConstant.WarningMessage] = "Invalid Operation";
            }

            var ordersToShip = await shipperService.GetOrdersToShip(currentUser.Id);

            return View(ordersToShip);
        }
        public async Task<IActionResult> FinnishOrder(string orderId)
        {
            if (orderId == null)
            {
                return Redirect(nameof(ShipperOrders));
            }

            if (await shipperService.FinishOrder(orderId))
            {
                return Redirect(nameof(ShipperOrders));
            }

            return Redirect(nameof(ShipperOrders));
        }
    }
}