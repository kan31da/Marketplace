using Marketplace.Core.Constants;
using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
using Marketplace.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserEditViewModel model)
        {

            if (!ModelState.IsValid || id != model.Id)
            {
                return View(model);
            }

            if (await userService.EditUser(model))
            {
                ViewData[MessageConstant.SuccessMessage] = "Edit Success";
            }
            else
            {
                ViewData[MessageConstant.WarningMessage] = "Invalid Changes";
            }

            return View(model);
        }

        public async Task<IActionResult> Roles(string id)
        {

            var user = await userService.GetApplicationUserById(id);

            var model = new UserRolesViewModel()
            {
                Id = user.Id,
                Name = $"{user.FirstName} {user.LastName}"
            };

            ViewBag.RoleItems = roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Id,
                    Selected = userManager.IsInRoleAsync(user, r.Name).Result
                });

            return View(model);
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
