using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Contracts
{
    public interface ICartService
    {
        Task<int> GetUsersCartCount(string id);
    }
}
