using Marketplace.Core.Constants;
using Marketplace.Core.Contracts;
using Marketplace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public UserController(RoleManager<IdentityRole> _roleManager,
            IUserService _userService,
            UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userService = _userService;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = UserConstants.Roles.Administrator)]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await userService.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await userService.GetUsersToEdit(id);

            return View(viewModel);
        }

        public async Task<IActionResult> CreateRole() //string roleName
        {
            //await roleManager.CreateAsync(new IdentityRole
            //{
            //    //Name = roleName
            //    Name = "Administrator"
            //});

            return Ok();
        }
    }
}
