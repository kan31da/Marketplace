using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    public class ContactsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
