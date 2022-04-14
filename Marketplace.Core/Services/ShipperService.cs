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

        public async Task<bool> AddOrderToShipper(string userId, string orderId)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(userId);

            var order = await repo.All<Order>()
                .Where(o => o.Id == Guid.Parse(orderId))
                .Include(o => o.Products)
                .Include(o => o.Shipper)
                .FirstOrDefaultAsync();

            if (order == null || user == null)
            {
                return false;
            }

            try
            {
                var shiper = new Shipper()
                {
                    UserId = Guid.Parse(user.Id),
                    FirstName = user.FirstName,
                    Phone = user.PhoneNumber,
                    LastName = user.LastName
                };

                await repo.AddAsync(shiper);

                order.Shipper = shiper;
                order.OrderStatus = GlobalConstants.Order.ORDER_STATUS_IN_DELIVERY;

                repo.Update(order);

                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> FinishOrder(string orderId)
        {
            var order = await repo.All<Order>()
              .Where(o => o.Id == Guid.Parse(orderId))
              .FirstOrDefaultAsync();

            if (order == null)
            {
                return false;
            }

            try
            {
                order.DeliveryDate = DateTime.Now;
                order.OrderStatus = GlobalConstants.Order.ORDER_STATUS_FINISHED;

                await repo.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<OrderDetailsViewModel> GetOrderDetails(string orderId)
        {

            var order = repo.All<Order>()
                .Where(o => o.Id.ToString() == orderId)
                .Select(o => new OrderDetailsViewModel()
                {
                    OrderId = o.Id.ToString(),
                    OrderStatus = o.OrderStatus,
                    OrderDate = o.OrderDate.ToString(GlobalConstants.Date.DATETIME_FORMAT),
                    Products = o.Products.Select(p => new CartProductsViewModel()
                    {
                        Id = p.Id.ToString(),
                        Image = p.ImagePath,
                        Name = p.Name,
                        Price = p.Price,
                        Quantity = p.Quantity,
                        TotalPrice = p.Price * p.Quantity
                    }).ToList(),
                    ShipperName = o.Shipper == null ? "" : $"{o.Shipper.FirstName} {o.Shipper.LastName}",
                    ShipperPhone = o.Shipper == null ? "" : o.Shipper.Phone,
                    OrderPrice = o.Products.Sum(m => m.Price * m.Quantity)

                }).FirstOrDefault();

            if (order == null)
            {
                return null;
            }

            return order;
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
            var users = await repo.All<ApplicationUser>()
                .ToListAsync();

            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            var orders = await repo.All<Order>()
                .Include(o => o.Products)
                .Include(o => o.Shipper)
                .ToListAsync();


            if (users == null || orders == null)
            {
                return null;
            }

            var result = new List<OrdersViewModel>();


            foreach (var o in orders)
            {
                if (o.OrderStatus == GlobalConstants.Order.ORDER_STATUS_IN_DELIVERY && o.Shipper.UserId.ToString() == user.Id)
                {
                    foreach (var u in users)
                    {
                        if (o.UserId == u.Id)
                        {
                            result.Add(new OrdersViewModel()
                            {
                                OrderId = o.Id.ToString(),
                                DeliveryAddress = o.DeliveryAddress,
                                OrderPrice = o.Products.Sum(p => p.Quantity * p.Price),
                                Phone = u.PhoneNumber,
                                UserName = $"{u.FirstName} {u.LastName}",
                                OrderDate = o.OrderDate.ToString(GlobalConstants.Date.DATETIME_FORMAT)
                            });
                        }
                    }
                }
            }

            return result;
        }

        public async Task<IEnumerable<ShipersListViewModel>> GetShippers(string orderId)
        {
            return await repo.All<ApplicationUser>()
                .Where(s => s.Is_Deleted == true)
               .Select(u => new ShipersListViewModel()
               {
                   Id = u.Id,
                   Email = u.Email,
                   Name = $"{u.FirstName} {u.LastName}",
                   Phone = u.PhoneNumber,
                   OrderId = orderId
               }).ToListAsync();
        }

        public async Task<bool> RemoveOrder(string orderId)
        {
            var order = await repo.All<Order>()
                .Where(o => o.Id.ToString() == orderId)
                .Include(o => o.Products)
                .FirstOrDefaultAsync();

            var products = await repo.All<Product>()
                .Include(i => i.Images).ToListAsync();

            if (order == null || products == null)
            {
                return false;
            }

            try
            {

                foreach (var item in products)
                {
                    foreach (var orderProduct in order.Products)
                    {
                        if (item.Id == orderProduct.ProductId)
                        {
                            item.Quantity += orderProduct.Quantity;
                        }
                    }
                }

                repo.UpdateRange<Product>(products);

                await repo.DeleteAsync<Order>(order.Id);

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
