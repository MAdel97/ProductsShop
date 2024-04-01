using ProductsShop.BL;
using ProductsShop.Common;
using ProductsShop.DTO;
using ProductsShop.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsShop.Models;

namespace ProductsShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private ProductsBLL _productsBll=null;

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _productsBll = new ProductsBLL();
            _logger = logger;
        }

        [HttpPost("AddProduct")]

        public async Task<Product> AddProduct([FromBody] ProductDTO product)
            {

            GenaricResponse<int> response = new GenaricResponse<int>();
            Status status = new Status  
            {
                Errors = null,
                Reason = "",
                StatusCode = 200
            };
            response.status = status;

            return await _productsBll.AddProduct(product);
        }
        [HttpGet("GetProducts")]
        public async Task<List<ProductDTO>> GetAllProducts()
        {
            

            GenaricResponse<int> response = new GenaricResponse<int>();
            Status status = new Status
            {
                Errors = null,
                Reason = "",
                StatusCode = 200
            };
            response.status = status;
            return await _productsBll.GetProducts();
            }

      
        [HttpPost("UpdateProduct")]
        public async Task<ProductDTO> UpdateProduct([FromBody] ProductDTO productDTO)
         {
            
            GenaricResponse<int> response = new GenaricResponse<int>();
            Status status = new Status
            {
                Errors = null,
                Reason = "",
                StatusCode = 200
            };
            response.status = status;
            return await _productsBll.UpdateProduct(productDTO);
        }

        [HttpPost("DeleteProduct")]
        public async Task<ProductDTO> DeleteCourse([FromBody] ProductDTO productDTO)
        {
           
            GenaricResponse<int> response = new GenaricResponse<int>();
            Status status = new Status
            {
                Errors = null,
                Reason = "",
                StatusCode = 200
            };
            response.status = status;
            return await _productsBll.DeleteProduct(productDTO.Id);
        }
    }
}
