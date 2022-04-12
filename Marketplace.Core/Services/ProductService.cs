using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
using Marketplace.Core.Utilities;
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
    public class ProductService : IProductService
    {
        private readonly IApplicatioDbRepository repo;

        public ProductService(IApplicatioDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<bool> AddImage(string id, string imagePath)
        {
            var product = await repo.All<Product>()
                .Where(p => p.Id.ToString() == id)
                .Include(p => p.Images)
                .FirstOrDefaultAsync();

            if (product == null || imagePath == null)
            {
                return false;
            }

            try
            {
                var image = new Image() { ImagePath = imagePath };

                await repo.AddAsync<Image>(image);

                product.Images.Add(image);

                repo.Update<Product>(product);

                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddProduct(AddProductViewModel model)
        {

            try
            {
                var product = new Product()
                {
                    Description = model.Description,
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = model.Quantity
                };

                product.Images.Add(new Image() { ImagePath = model.FirstImagePath });

                await repo.AddAsync<Product>(product);

                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteImage(string id, string imageToDelete)
        {
            var product = await repo.All<Product>()
               .Where(p => p.Id.ToString() == id)
               .Include(p => p.Images)
               .FirstOrDefaultAsync();

            var image = await repo.All<Image>()
              .Where(i => i.Id.ToString() == imageToDelete)
              .FirstOrDefaultAsync();

            if (product == null || image == null)
            {
                return false;
            }

            try
            {
                product.Images.Remove(image);

                repo.Update<Product>(product);

                await repo.DeleteAsync<Image>(image.Id);

                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var product = await repo.All<Product>()
              .Where(p => p.Id.ToString() == id)
              .Include(p => p.Images)
              .FirstOrDefaultAsync();

            if (product == null)
            {
                return false;
            }

            var images = product.Images.ToList();

            try
            {
                product.Images.Clear();

                await repo.DeleteAsync<Product>(product.Id);

                repo.DeleteRange<Image>(images);

                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<ProductDetailsViewModel> GetProductDetails(string id)
        {
            var product = await repo.All<Product>()
              .Where(p => p.Id.ToString() == id)
              .Select(p => new ProductDetailsViewModel()
              {
                  Id = p.Id.ToString(),
                  Name = p.Name,
                  Description = p.Description,
                  Price = p.Price,
                  Quantity = p.Quantity,
                  Images = p.Images.Select(i => new ImageViewModel()
                  {
                      Id = i.Id.ToString(),
                      ImagePath = i.ImagePath
                  })

              }).FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<ProductListViewModel>> GetProducts()
        {
            return await repo.All<Product>()
                .Where(p => p.Quantity > GlobalConstants.CartProduct.ZERO_QUANTITY)
                .Select(p => new ProductListViewModel()
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Price = p.Price.ToString(),
                    Quantity = p.Quantity.ToString(),
                    Image = p.Images.Select(i => i.ImagePath).FirstOrDefault() ?? ""

                }).ToListAsync();
        }

        public async Task<IEnumerable<ProductListViewModel>> GetProductsWithZeroQuantity()
        {
            return await repo.All<Product>()
                .Where(p => p.Quantity == GlobalConstants.CartProduct.ZERO_QUANTITY)
                .Select(p => new ProductListViewModel()
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Price = p.Price.ToString(),
                    Quantity = p.Quantity.ToString(),
                    Image = p.Images.Select(i => i.ImagePath).FirstOrDefault() ?? ""

                }).ToListAsync();
        }

        public async Task<ProductEditViewModel> GetProductToEdit(string id)
        {
            var product = await repo.All<Product>()
              .Where(p => p.Id.ToString() == id)
              .Select(p => new ProductEditViewModel()
              {
                  Id = p.Id.ToString(),
                  Name = p.Name,
                  Description = p.Description,
                  Price = p.Price,
                  Quantity = p.Quantity

              }).FirstOrDefaultAsync();

            return product;
        }

        public async Task<ProductToEditImagesViewModel> GetProductToEditImages(string id)
        {

            var product = await repo.All<Product>()
                .Where(p => p.Id.ToString() == id)
                .Select(p => new ProductToEditImagesViewModel()
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Images = p.Images.Select(i => new ImageViewModel()
                    {
                        Id = i.Id.ToString(),
                        ImagePath = i.ImagePath
                    })

                }).FirstOrDefaultAsync();

            return product;
        }

        public async Task<bool> ProductToEdit(ProductEditViewModel model)
        {
            var product = await repo.All<Product>()
             .Where(p => p.Id.ToString() == model.Id)
             .FirstOrDefaultAsync();

            if (product == null)
            {
                return false;
            }

            try
            {
                product.Name = model.Name;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.Description = model.Description;

                repo.Update<Product>(product);

                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
