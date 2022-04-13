using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
using Marketplace.Core.Utilities;
using Marketplace.Infrastructure.Data.Identity;
using Marketplace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Core.Services
{
    public class UserService : IUserService
    {

        private readonly IApplicatioDbRepository repo;

        public UserService(IApplicatioDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<bool> EditUser(UserEditViewModel model)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

            if (user == null)
            {
                return false;
            }

            try
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.Is_Deleted = bool.Parse(model.Is_Deleted);

                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<OrdersViewModel>> GetOrders(string id)
        {
            var user = await repo.All<ApplicationUser>()
           .Where(u => u.Id == id)
           .Include(u => u.Orders)
           .ThenInclude(u => u.Products)
           .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            return user.Orders
                .Where(o => o.OrderStatus == GlobalConstants.Order.ORDER_STATUS_IN_PROGRESS)
                .Select(o => new OrdersViewModel()
                {
                    OrderId = o.Id.ToString(),
                    DeliveryAddress = o.DeliveryAddress,
                    OrderPrice = o.Products.Sum(p => p.Quantity * p.Price),
                    Phone = user.PhoneNumber,
                    UserName = $"{user.FirstName} {user.LastName}",
                    OrderDate = o.OrderDate.ToString(GlobalConstants.Date.DATETIME_FORMAT)

                }).ToList();

        }

        public async Task<IEnumerable<OrderHistoryViewModel>> GetOrdersHistory(string id)
        {
            var user = await repo.All<ApplicationUser>()
              .Where(u => u.Id == id)
              .Include(u => u.Orders)
              .ThenInclude(u => u.Products)
              .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            return user.Orders
                .Where(o => o.OrderStatus == GlobalConstants.Order.ORDER_STATUS_FINISHED)
                .Select(o => new OrderHistoryViewModel()
                {
                    OrderId = o.Id.ToString(),
                    DeliveryAddress = o.DeliveryAddress,
                    OrderPrice = o.Products.Sum(p => p.Quantity * p.Price),
                    Phone = user.PhoneNumber,
                    UserName = $"{user.FirstName} {user.LastName}",
                    OrderDate = o.OrderDate.ToString(GlobalConstants.Date.DATETIME_FORMAT),
                    DeliveryDate = o.DeliveryDate?.ToString(GlobalConstants.Date.DATETIME_FORMAT) ?? "",
                    OrderStatus = GlobalConstants.Order.ORDER_STATUS_FINISHED

                }).ToList();
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await repo.GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = $"{u.FirstName} {u.LastName}"
                }).ToListAsync();
        }

        public async Task<UserEditViewModel> GetUsersToEdit(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            return new UserEditViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Is_Deleted = user.Is_Deleted.ToString()
            };
        }

        public async Task<bool> SelfEditUser(UserEditViewModel model)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

            if (user == null)
            {
                return false;
            }

            try
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;

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
