using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageProducts()
        {
            return View();
        }
    }
}
