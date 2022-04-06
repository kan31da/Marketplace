using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
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
            var product = new Product()
            {
                
            };

            try
            {

                repo.AddAsync<Product>(product);

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
                     Quantity = p.Price.ToString()

                 }).ToListAsync();
        }
    }
}
