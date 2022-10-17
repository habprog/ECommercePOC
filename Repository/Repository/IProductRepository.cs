using Helper.Wrappers;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public interface IProductRepository
    {
        Task<Response<dynamic>> GetProducts();
        Task<Response<dynamic>> Checkout(string id);
    }
}
