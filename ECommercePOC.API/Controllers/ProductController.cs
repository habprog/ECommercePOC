using Helper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Repositories.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommercePOC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response<dynamic>> GetPproducts()
        {
            var response = await _productRepository.GetProducts();
            return response;
        }

        [HttpGet("checkout/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Response<dynamic>> ProductCheckout(string id)
        {
            var response = await _productRepository.Checkout(id);
            return response;
        }
    }
}
