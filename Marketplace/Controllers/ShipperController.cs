using Marketplace.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    [Authorize]
    [Authorize(Roles = UserConstants.Roles.Shipper)]
    public class ShipperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
