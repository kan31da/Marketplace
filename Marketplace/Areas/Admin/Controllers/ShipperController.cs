using Marketplace.Core.Contracts;
using Marketplace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Areas.Admin.Controllers
{
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

        public async Task<IActionResult> Orders()
        {
            
            var orders = await shipperService.GetOrders();

            if (orders == null)
            {
                return Redirect("/");
            }

            return View(orders);
        }

      
        public async Task<IActionResult> AddShipperOrder(string shiperId, string orderId)
        {
            //var currentUser = await userManager.GetUserAsync(User);

            //if (currentUser == null)
            //{
            //    return RedirectToAction(nameof(Index));
            //}

            //var ordersToShip = await shipperService.GetOrdersToShip(currentUser.Id);

            return Ok();
        }


        public async Task<IActionResult> RemoveOrder(string shiperId, string orderId)
        {
            //var currentUser = await userManager.GetUserAsync(User);

            //if (currentUser == null)
            //{
            //    return RedirectToAction(nameof(Index));
            //}

            //var ordersToShip = await shipperService.GetOrdersToShip(currentUser.Id);

            return Ok();
        }
    }
}
