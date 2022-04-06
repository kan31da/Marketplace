using Marketplace.Core.Models;

namespace Marketplace.Core.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListViewModel>> GetProducts();
        Task<bool> AddProduct(AddProductViewModel model);
    }
}
