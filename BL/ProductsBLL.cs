using ProductsShop.Common;
using ProductsShop.DTO;
using ProductsShop.Helper;
using ProductsShop.Interfaces;
using ProductsShop.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsShop.Models;

namespace ProductsShop.BL
{
    public class ProductsBLL : DTOMapper
    {
        private IProductsRepository repository = null;
        private readonly IConfiguration _configuration;
        public ProductsBLL()
        {

            this.repository = new ProductsRepository( );
        }

        public async Task<Product> AddProduct(ProductDTO productdto )
        {
            var obj = DTOMapper.mapper.Map<Product>(productdto);

            return await repository.AddProduct(obj);
           

        }

        public async Task<bool> ValidateUserPassword(UserDTO userDTO)
        {
            try
            {
                var data = await repository.GetUserDataById(userDTO.Name);
                if (data.Password == userDTO.Password && data.Name == userDTO.Name)
                {

                    await repository.UpdateUser(data);
                    return true;
                }
            
                else { return false; }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
               
           
        }

        public async Task<User> AddUser(UserDTO user) 
        {
            var obj = DTOMapper.mapper.Map<User>(user);

            return await repository.AddUser(obj);


        }
        public async Task<List<ProductDTO>> GetProducts()
        {
            var products = await repository.GetAllProducts();

            var objDTO = DTOMapper.mapper.Map<List<ProductDTO>>(products);
            return objDTO;
        }
  
        public async Task<ProductDTO> UpdateProduct(ProductDTO productDTO )
        {
            int id = productDTO.Id;
            var obj = DTOMapper.mapper.Map<Product>(productDTO);

            var product = await repository.UpdateProduct(obj,id);

            var objDTO = DTOMapper.mapper.Map<ProductDTO>(product);
            return objDTO;          
        }

        public async Task<ProductDTO> DeleteProduct(int Id)
        {
            var product = await repository.DeleteProduct(Id);

            var objDTO = DTOMapper.mapper.Map<ProductDTO>(product);
            return objDTO;
        }
    }
}

