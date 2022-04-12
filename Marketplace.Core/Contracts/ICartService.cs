using Marketplace.Core.Models;

namespace Marketplace.Core.Contracts
{
    public interface ICartService
    {
        Task<int> GetUsersCartCount(string id);
        Task<OrderProductViewModel> GetUsersCurrentOrder(string userId);
        Task<bool> AddProductToCart(string userId, string productId);
        Task<bool> DeleteProductCart(string userId, string productId);
        Task<bool> CartProductToEdit(string userId, string productId, int quantity);
        Task<bool> OrderProductCart(string userId, string deliveryAddress);
        Task<CartProductsViewModel> GetProductToEdit(string userId, string productId);
        Task<IEnumerable<CartProductsViewModel>> GetCartProducts(string userId);
    }
}
