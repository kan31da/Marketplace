using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
using Marketplace.Core.Services;
using Marketplace.Infrastructure.Data.Identity;
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
    public class ShipperServiceTests
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
                .AddSingleton<IShipperService, ShipperService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();

            await SeedDbAsync(repo);
        }

        [Test]
        public void AddOrderToShipperTest()
        {
            var service = serviceProvider.GetService<IShipperService>();

            Assert.That(async Task<bool> () => await service.AddOrderToShipper("asd", "asd"), Is.EqualTo(false));
        }

        [Test]
        public void FinishOrderTest()
        {
            var service = serviceProvider.GetService<IShipperService>();

            Assert.That(async () => await service.FinishOrder(""), Is.EqualTo(false));
        }

        [Test]
        public void GetOrderDetailsTest()
        {
            var service = serviceProvider.GetService<IShipperService>();

            var order = new OrderDetailsViewModel();           

            Assert.That(async () => await service.GetOrderDetails(""), Is.EqualTo(order));
        }

        [Test]
        public void GetOrdersTest()
        {
            var service = serviceProvider.GetService<IShipperService>();
            var result = new List<OrdersViewModel>();
            Assert.That(async () => await service.GetOrders(), Is.EqualTo(result));
        }

        [Test]
        public void GetOrdersToShipTest()
        {
            var service = serviceProvider.GetService<IShipperService>();
            var result = new List<OrdersViewModel>();
            Assert.That(async () => await service.GetOrdersToShip(""), Is.EqualTo(result));
        }


        [Test]
        public void GetShippersTest()
        {
            var service = serviceProvider.GetService<IShipperService>();
            var result = new List<ShipersListViewModel>();
            Assert.That(async () => await service.GetOrdersToShip(""), Is.EqualTo(result));
        }

        [Test]
        public void RemoveOrderTest()
        {
            var service = serviceProvider.GetService<IShipperService>();

            Assert.That(async () => await service.RemoveOrder(""), Is.EqualTo(false));
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
                Quantity = 2
            };

            product.Images.Add(new Image() { ImagePath = "Image" });

            var user = new ApplicationUser()
            {
                Id = "asd"
            };

            await repo.AddAsync(product);
            await repo.AddAsync(user);

            await repo.SaveChangesAsync();
        }
    }
}
