using ProductsShop.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsShop.Models;

namespace ProductsShop.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product> AddProduct(Product _obj);
        Task<List<Product>> GetAllProducts();
        Task<Product> UpdateProduct(Product product,int id);
        Task<Product> DeleteProduct(int Id);
        Task<User> AddUser (User userDTO);
        Task<User> UpdateUser(User user);

        Task<User> GetUserDataById(string Email);


    }
}
