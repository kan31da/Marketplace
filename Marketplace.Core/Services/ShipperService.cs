using Marketplace.Core.Contracts;
using Marketplace.Core.Models;
using Marketplace.Core.Utilities;
using Marketplace.Infrastructure.Data.Identity;
using Marketplace.Infrastructure.Data.Models;
using Marketplace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IApplicatioDbRepository repo;

        public ShipperService(IApplicatioDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<OrdersViewModel>> GetOrders()
        {
            var orders = await repo.All<ApplicationUser>()
                .Include(u => u.Orders)
                .ThenInclude(u => u.Products)
                .Select(o => o.Orders
                .Where(p => p.OrderStatus == GlobalConstants.Order.ORDER_STATUS_IN_PROGRESS)
                .Select(i => new OrdersViewModel()
                {
                    Phone = o.PhoneNumber,
                    UserName = $"{o.FirstName} {o.LastName}",
                    DeliveryAddress = i.DeliveryAddress,
                    OrderDate = i.OrderDate.ToString(GlobalConstants.Date.DATETIME_FORMAT),
                    OrderPrice = i.Products.Sum(q => q.Quantity * q.Price),
                    OrderId = i.Id.ToString()
                }))
                .ToListAsync();

            if (orders == null)
            {
                return null;
            }

            var result = new List<OrdersViewModel>();

            foreach (var el in orders)
            {
                foreach (var item in el)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public async Task<IEnumerable<OrdersViewModel>> GetOrdersToShip(string id)
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
                .Where(o => o.OrderStatus == GlobalConstants.Order.ORDER_STATUS_IN_DELIVERY)
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
    }
}
