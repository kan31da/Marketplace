using Marketplace.Core.Models;

namespace Marketplace.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();
        Task<UserEditViewModel> GetUsersToEdit(string id);
        Task<bool> EditUser(UserEditViewModel model);
    }
}
