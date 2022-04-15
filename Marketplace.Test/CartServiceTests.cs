using Marketplace.Core.Contracts;
using Marketplace.Core.Services;
using Marketplace.Infrastructure.Data.Models;
using Marketplace.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Marketplace.Test
{
    public class CartServiceTests
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
                .AddSingleton<ICartService, CartService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();

            await SeedDbAsync(repo);

        }

        [Test]
        public void GetUsersCartCountTest()
        {

            var service = serviceProvider.GetService<ICartService>();

            Assert.That(async () => await service.GetUsersCartCount(""), Is.EqualTo(0));
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


            await repo.AddAsync(product);
            await repo.SaveChangesAsync();
        }
    }
}