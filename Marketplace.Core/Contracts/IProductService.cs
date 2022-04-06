using Marketplace.Core.Models;

namespace Marketplace.Core.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListViewModel>> GetProducts();
        Task<ProductToEditViewModel> GetProductToEdit(string id);
        Task<bool> AddProduct(AddProductViewModel model);
    }
}
