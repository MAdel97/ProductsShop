using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsShop.BL;
using ProductsShop.Common;
using ProductsShop.DTO;
using ProductsShop.Helper;
using ProductsShop.Models;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsShop.Controllers
{
    [Route("[controller]")]

    [ApiController]
    public class UserController : ControllerBase
    {
       
        private ProductsBLL _productsBll = null;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _productsBll = new ProductsBLL();
            _logger = logger;
        }
        [HttpPost("signup")]
        public async Task<GenaricResponse<User>> AddUser([FromBody] UserDTO userDTO)
        {
            GenaricResponse<User> response = new GenaricResponse<User>();
            Status status = new Status
            {
                Errors = null,
                Reason = "",
                StatusCode = 200
            };
            response.status = status;
            response.item=await _productsBll.AddUser(userDTO);
            return response;
        }

        [HttpPost("signin")]
        public async  Task<GenaricResponse<bool>> ValidateUser([FromBody] UserDTO userDTO)
        {
            GenaricResponse<bool> response = new GenaricResponse<bool>();
            Status status = new Status
            {
                Errors = null,
                Reason = "",
                StatusCode = 200
            };
            response.status = status;
           
            var valid = await _productsBll.ValidateUserPassword(userDTO);
            if (!valid)
            {
                response.status.StatusCode = 401;
                response.item = false;


            }
            else 
            response.item = true;
            return response;
           
        }

    }
}
