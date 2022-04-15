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
    public class UserServiceTests
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
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();

            await SeedDbAsync(repo);



        }

        [Test]
        public void EditUserTest()
        {
            var service = serviceProvider.GetService<IUserService>();
            UserEditViewModel user = new UserEditViewModel();

            Assert.That(async () => await service.EditUser(user), Is.EqualTo(false));
        }

        [Test]
        public void EditUserTest2()
        {
            var service = serviceProvider.GetService<IUserService>();
            var user = new UserEditViewModel() { Id = "asd", FirstName = "asd", Email = "asd", Is_Shipper = "true", LastName = "asd", PhoneNumber = "0888857265" };

            Assert.That(async () => await service.EditUser(user), Is.EqualTo(true));
        }

        [Test]
        public void GetOrdersTest()
        {
            var service = serviceProvider.GetService<IUserService>();

            Assert.That(async () => await service.GetOrders(""), Is.EqualTo(null));
        }

        [Test]
        public void GetOrdersTest2()
        {
            var service = serviceProvider.GetService<IUserService>();
            var orders = new List<OrdersViewModel>();
            Assert.That(async () => await service.GetOrders("asd"), Is.EqualTo(orders));
        }

        [Test]
        public void GetOrderDetailsTest()
        {
            var service = serviceProvider.GetService<IUserService>();

            Assert.That(async () => await service.GetOrderDetails("asd", ""), Is.EqualTo(null));
        }

        [Test]
        public void GetOrdersHistoryTest()
        {
            var service = serviceProvider.GetService<IUserService>();

            Assert.That(async () => await service.GetOrdersHistory(""), Is.EqualTo(null));
        }

        [Test]
        public void GetOrdersHistoryTest2()
        {
            var service = serviceProvider.GetService<IUserService>();
            var orders = new List<OrderHistoryViewModel>();
            Assert.That(async () => await service.GetOrdersHistory("asd"), Is.EqualTo(orders));
        }

        [Test]
        public void GetUserByIdTest()
        {
            var service = serviceProvider.GetService<IUserService>();

            Assert.That(async () => await service.GetUserById(""), Is.EqualTo(null));
        }

        [Test]
        public void GetUsersTest()
        {
            var service = serviceProvider.GetService<IUserService>();
            var users = new List<UserListViewModel>();

            Assert.That(async () => await service.GetUsers(), Is.EqualTo(users));
        }

        [Test]
        public void GetUsersToEditTest()
        {
            var service = serviceProvider.GetService<IUserService>();
            var user = new UserEditViewModel();
            Assert.That(async Task<UserEditViewModel> () => await service.GetUsersToEdit(""), Is.EqualTo(null));
        }

        [Test]
        public void SelfEditUserTest()
        {
            var service = serviceProvider.GetService<IUserService>();
            var user = new UserEditViewModel();

            Assert.That(async () => await service.SelfEditUser(user), Is.EqualTo(false));
        }

        [Test]
        public void SelfEditUserTest2()
        {
            var service = serviceProvider.GetService<IUserService>();
            var user = new UserEditViewModel() { Id = "asd", FirstName = "asd" };

            Assert.That(async () => await service.SelfEditUser(user), Is.EqualTo(true));
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
