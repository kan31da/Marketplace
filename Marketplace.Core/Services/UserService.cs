using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
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

        public async Task<ApplicationUser> GetApplicationUserById(string id)
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
    }
}
