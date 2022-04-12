using Marketplace.Core.Models;

namespace Marketplace.Core.Contracts
{
    public interface ICartService
    {
        Task<int> GetUsersCartCount(string id);
        Task<bool> AddProductToCart(string userId, string productId);
        Task<bool> DeleteProductCart(string userId, string productId);
        Task<bool> CartProductToEdit(string userId, string productId, int quantity);
        Task<CartProductsViewModel> GetProductToEdit(string userId, string productId);
        Task<IEnumerable<CartProductsViewModel>> GetCartProducts(string userId);
    }
}
