using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
using Marketplace.Core.Services;
using Marketplace.Infrastructure.Data.Models;
using Marketplace.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Test
{
    public class ProductServiceTests
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicatioDbRepository, ApplicatioDbRepository>()
                .AddSingleton<IProductService, ProductService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();

            await SeedDbAsync(repo);

        }

        [Test]
        public void AddImageTest()
        {
            var service = serviceProvider.GetService<IProductService>();

            Assert.That(async () => await service.AddImage(null, null), Is.EqualTo(false));
        }

        [Test]
        public void AddImageTest2()
        {
            var service = serviceProvider.GetService<IProductService>();

            var id = Guid.Parse("53ccd525-e503-4877-ae1c-fe8c7d1bc231");

            Assert.That(async () => await service.AddImage(id.ToString(), "asd"), Is.EqualTo(true));
        }

        [Test]
        public void AddProductTest()
        {
            var service = serviceProvider.GetService<IProductService>();
            var p = new AddProductViewModel();
            Assert.That(async () => await service.AddProduct(p), Is.EqualTo(false));
        }

        [Test]
        public void DeleteImageTest()
        {
            var service = serviceProvider.GetService<IProductService>();

            Assert.That(async () => await service.DeleteImage("", ""), Is.EqualTo(false));
        }

        [Test]
        public void DeleteProductTest()
        {
            var service = serviceProvider.GetService<IProductService>();

            Assert.That(async () => await service.DeleteProduct(""), Is.EqualTo(false));
        }

        [Test]
        public void GetProductDetailsTest()
        {
            var service = serviceProvider.GetService<IProductService>();
            var list = new List<ProductListViewModel>();

            Assert.That(async () => await service.GetProductDetails(""), Is.EqualTo(null));
        }


        [Test]
        public void GetProductsTest()
        {
            var service = serviceProvider.GetService<IProductService>();
            var list = new List<ProductListViewModel>();
            Assert.That(async () => await service.GetProducts(), Is.EqualTo(list));
        }

        [Test]
        public void GetProductsWithZeroQuantityTest()
        {
            var service = serviceProvider.GetService<IProductService>();
            var list = new List<ProductListViewModel>();
            Assert.That(async () => await service.GetProductsWithZeroQuantity(), Is.EqualTo(list));
        }

        [Test]
        public void GetProductToEditTest()
        {
            var service = serviceProvider.GetService<IProductService>();

            Assert.That(async () => await service.GetProductToEdit(""), Is.EqualTo(null));
        }

        [Test]
        public void GetProductToEditImagesTest()
        {
            var service = serviceProvider.GetService<IProductService>();

            Assert.That(async () => await service.GetProductToEditImages(""), Is.EqualTo(null));
        }


        [Test]
        public void ProductToEditTest()
        {
            var service = serviceProvider.GetService<IProductService>();
            var p = new ProductEditViewModel();
            Assert.That(async () => await service.ProductToEdit(p), Is.EqualTo(false));
        }

        [Test]
        public void PProductToEditTest()
        {
            var service = serviceProvider.GetService<IProductService>();
          
            var p = new ProductEditViewModel();
            Assert.That(async () => await service.ProductToEdit(p), Is.EqualTo(false));
        }



        [Test]
        public void Test()
        {
            Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicatioDbRepository repo)
        {
            var product = new Product()
            {
                Description = "test",
                Name = "product",
                Price = 12.00m,
                Quantity = 2,
                Id = Guid.Parse("53ccd525-e503-4877-ae1c-fe8c7d1bc231")
            };

            product.Images.Add(new Image() { ImagePath = "Image" });


            await repo.AddAsync(product);
            await repo.SaveChangesAsync();
        }
    }
}
