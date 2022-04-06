using Marketplace.Core.Models;

namespace Marketplace.Core.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListViewModel>> GetProducts();
        Task<IEnumerable<ProductListViewModel>> AddProduct();
        Task<bool> CreateCategory(string categoryLabel);
    }
}
