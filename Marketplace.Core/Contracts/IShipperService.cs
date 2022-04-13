using Marketplace.Core.Models;

namespace Marketplace.Core.Contracts
{
    public interface IShipperService
    {
        Task<IEnumerable<OrdersViewModel>> GetOrdersToShip(string id);
        Task<IEnumerable<OrdersViewModel>> GetOrders();
        Task<IEnumerable<ShipersListViewModel>> GetShippers(string orderId);
        Task<bool> FinishOrder(string orderId);
        Task<bool> RemoveOrder(string orderId);
        Task<bool> AddOrderToShipper(string userId, string orderId);
        Task<OrderDetailsViewModel> GetOrderDetails(string orderId);
    }
}
