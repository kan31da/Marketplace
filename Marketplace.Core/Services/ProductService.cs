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

        public async Task<bool> AddProduct(AddProductViewModel model)
        {

            try
            {
                var product = new Product()
                {
                    Description = model.Description,
                    Name = model.Name,
                    Price = decimal.Parse(model.Price),
                    Quantity = int.Parse(model.Quantity)
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

        public async Task<IEnumerable<ProductListViewModel>> GetProducts()
        {
            return await repo.All<Product>()
                 .Select(p => new ProductListViewModel()
                 {
                     Id = p.Id.ToString(),
                     Name = p.Name,
                     Price = p.Price.ToString(),
                     Quantity = p.Price.ToString(),
                     Image = p.Images.Select(i => i.ImagePath).First() ?? ""

                 }).ToListAsync();
        }

        public async Task<ProductToEditViewModel> GetProductToEdit(string id)
        {

            var product = await repo.All<Product>()
                .Where(p => p.Id.ToString() == id)
                 .Select(p => new ProductToEditViewModel()
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
    }
}
