using Marketplace.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Areas.Admin.Controllers
{
    [Authorize(Roles = UserConstants.Roles.Administrator)]
    [Area(UserConstants.Area.Admin)]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
