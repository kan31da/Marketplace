using Marketplace.Core.Models;
using Marketplace.Infrastructure.Data.Identity;

namespace Marketplace.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();
        Task<IEnumerable<OrdersViewModel>> GetOrders(string id);
        Task<IEnumerable<OrderHistoryViewModel>> GetOrdersHistory(string id);
        Task<UserEditViewModel> GetUsersToEdit(string id);
        Task<bool> EditUser(UserEditViewModel model);
        Task<bool> SelfEditUser(UserEditViewModel model);
        Task<ApplicationUser> GetUserById(string id);       
    }
}
