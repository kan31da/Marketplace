using Marketplace.Core.Constants;
using Marketplace.Core.Contracts;
using Marketplace.Infrastructure.Data.Identity;
using Marketplace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IApplicatioDbRepository repo;


        public CartService(IApplicatioDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<int> GetUsersCartCount(string id)
        {
            if (id == null)
            {
                return CartConstants.Cart_Count_Zero;
            }

            var user = await repo.All<ApplicationUser>()
            .Where(u => u.Id == id)
            .Include(u => u.Cart)
            .ThenInclude(u => u.CartItems)
            .ThenInclude(u => u.Product)
            .FirstOrDefaultAsync();

            return user?.Cart.CartItems.Count() ?? CartConstants.Cart_Count_Zero;
        }
    }
}
