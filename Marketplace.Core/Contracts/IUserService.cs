using Marketplace.Core.Models;
using Marketplace.Infrastructure.Data.Identity;

namespace Marketplace.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();
        Task<UserEditViewModel> GetUsersToEdit(string id);
        Task<bool> EditUser(UserEditViewModel model);
        Task<ApplicationUser> GetApplicationUserById(string id);
    }
}
