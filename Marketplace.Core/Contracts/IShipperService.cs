using Marketplace.Core.Models;

namespace Marketplace.Core.Contracts
{
    public interface IShipperService
    {
        Task<IEnumerable<OrdersViewModel>> GetOrdersToShip(string id);
        Task<IEnumerable<OrdersViewModel>> GetOrders();
    }
}
