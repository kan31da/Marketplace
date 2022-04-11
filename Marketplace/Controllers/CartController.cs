using Marketplace.Core.Constants;
using Marketplace.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    [Authorize]
    [Authorize(Roles = UserConstants.Roles.User)]
    public class CartController : BaseController
    {
        private readonly ICartService cartService;

        public CartController(ICartService _cartService)
        {
            cartService = _cartService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProductToCart(string id)
        {
            return Ok();
        }
    }
}
