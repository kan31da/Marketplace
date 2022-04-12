using Marketplace.Core.Constants;
using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
using Marketplace.Core.Utilities;
using Marketplace.Infrastructure.Data.Identity;
using Marketplace.Infrastructure.Data.Models;
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

        public async Task<bool> AddProductToCart(string userId, string productId)
        {
            var product = await repo.All<Product>()
               .Where(p => p.Id.ToString() == productId)
               .Include(p => p.Images)
               .FirstOrDefaultAsync();

            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(u => u.Products)
                .FirstOrDefaultAsync();

            if (product == null || user == null)
            {
                return false;
            }

            try
            {
                if (user.Cart.Products.Any(p => p.ProductId == product.Id))
                {
                    var userProduct = user.Cart.Products.FirstOrDefault(p => p.ProductId == product.Id);

                    if (userProduct == null)
                    {
                        return false;
                    }

                    if (product.Quantity > 0)
                    {
                        userProduct.Quantity++;
                        product.Quantity--;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var addProduct = new CartProduct()
                    {
                        ProductId = product.Id,
                        ImagePath = product.Images.Select(i => i.ImagePath).First(),
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = GlobalConstants.CartProduct.FIRST_QUANTITY
                    };

                    await repo.AddAsync<CartProduct>(addProduct);

                    user.Cart.Products.Add(addProduct);
                }



                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CartProductToEdit(string userId, string productId, int quantity)
        {

            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(u => u.Products)
                .FirstOrDefaultAsync();

            if (productId == null || user == null || quantity == null || quantity < 1)
            {
                return false;
            }

            var userProduct = user.Cart.Products
               .Where(p => p.Id.ToString() == productId)
               .FirstOrDefault();

            var product = await repo.All<Product>()
               .Where(p => p.Id == userProduct.ProductId)
               .Include(p => p.Images)
               .FirstOrDefaultAsync();

            if (userProduct == null || product == null)
            {
                return false;
            }

            try
            {

                var difrents = userProduct.Quantity - quantity;

                product.Quantity += difrents;

                if (product.Quantity < GlobalConstants.CartProduct.ZERO_QUANTITY)
                {
                    return false;
                }

                userProduct.Quantity = quantity;

                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProductCart(string userId, string productId)
        {
            var user = await repo.All<ApplicationUser>()
               .Where(u => u.Id == userId)
               .Include(u => u.Cart)
               .ThenInclude(u => u.Products)
               .FirstOrDefaultAsync();

            var userProduct = user.Cart.Products.Where(p => p.Id.ToString() == productId).FirstOrDefault();

            var product = await repo.All<Product>()
              .Where(p => p.Id == userProduct.ProductId)
              .Include(p => p.Images)
              .FirstOrDefaultAsync();

            if (user == null || product == null || userProduct == null)
            {
                return false;
            }

            try
            {
                product.Quantity += userProduct.Quantity;

                user.Cart.Products.Remove(userProduct);

                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<CartProductsViewModel>> GetCartProducts(string userId)
        {
            var user = await repo.All<ApplicationUser>()
           .Where(u => u.Id == userId)
           .Include(u => u.Cart)
           .ThenInclude(u => u.Products)
           .FirstOrDefaultAsync();

            return user.Cart.Products
                  .Select(p => new CartProductsViewModel()
                  {
                      Id = p.Id.ToString(),
                      Name = p.Name,
                      Price = p.Price,
                      Quantity = p.Quantity,
                      Image = p.ImagePath,
                      TotalPrice = p.Price * p.Quantity

                  }).ToList();
        }

        public async Task<CartProductsViewModel> GetProductToEdit(string userId, string productId)
        {
            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.Cart)
                .ThenInclude(u => u.Products)
                .FirstOrDefaultAsync();

            var product = user.Cart.Products
                .Where(p => p.Id.ToString() == productId)
                .Select(p => new CartProductsViewModel()
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    Image = p.ImagePath,
                    TotalPrice = p.Price * p.Quantity

                }).FirstOrDefault();

            return product;
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
                .ThenInclude(u => u.Products)
                .FirstOrDefaultAsync();

            return user?.Cart.Products.Select(p => p.Quantity).Sum() ?? CartConstants.Cart_Count_Zero;
        }
    }
}
