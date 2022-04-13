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

        //public async Task<IActionResult> FinnishOrder(string orderId)
        //{
        //    var currentUser = await userManager.GetUserAsync(User);

        //    if (currentUser == null || orderId == null)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }


        //    if (true)
        //    {

        //    }

        //    var ordersToFinish = await shipperService.GetOrderToFinish(currentUser.Id, orderId);

        //    return View(ordersToShip);
        //}

    }
}
