using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    //[Authorize]
    //public class UserController : Controller
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateRole(string roleName)
        {
            await roleManager.CreateAsync(new IdentityRole
            {
                Name = roleName
                //Name = "Administrator"
            });

            return Ok();
        }
    }
}
