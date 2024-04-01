﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using ProductsShop.Interfaces;
using ProductsShop.Common;
using ProductsShop.ProductsShop.EntityModel;
using ProductsShop.DTO;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductsShop.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private ProductsShopContext _context = null;
        private DbSet<Product> _products = null;
        private DbSet<User> _users = null;

        private readonly IConfiguration _configuration;


        public ProductsRepository()
        {
            this._context = new ProductsShopContext();

            _products = _context.Set<Product>();
            _users =_context.Set<User>();
        }

        public async Task<Product> AddProduct(Product _obj)
        {

            DateTime DateTime_ = DateTime.Now;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _obj.Id = 0;

                    _products.Add(_obj);
                    await _context.SaveChangesAsync();
                    transaction.CommitAsync();
                }


                catch (Exception ex)
                {


                    Console.Write("An error occured");
                }
            }
            return _obj;
        }


        public async Task<User> AddUser(User _obj)
        {

            DateTime DateTime_ = DateTime.Now;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _obj.UserId = 0;
                
                    _users.Add(_obj);
                    await _context.SaveChangesAsync();
                    transaction.CommitAsync();
                }


                catch (Exception ex)
                {


                    Console.Write("An error occured");
                }
            }
            return _obj;
        }



        public async Task<List<Product>> GetAllProducts()
        {

            var list = new List<Product>();
            DateTime DateTime_ = DateTime.Now;

            try
            {


                list = await _context.products.ToListAsync();

            }
            catch (Exception ex)
            {
                Console.Write("An error occured");
            }

            return list;
        }


        public async Task<User> GetUserDataById(string Name)
        {

            var user = new User();
            var userpassword = "";
            DateTime DateTime_ = DateTime.Now;

            try
            {


                user =  await _context.users.Where(entity =>  entity.Name==Name).FirstAsync();
              

            }
            catch (Exception ex)
            {
                Console.Write("An error occured");
            }

            return user;
        }





        public async Task<Product> UpdateProduct(Product product,int ID)
        {

            DateTime DateTime_ = DateTime.Now;
            ///var course = new AcademicCourse();
            var result = new Product();
            try
            {
                product .Id= ID;

                result = _products.Update(product).Entity;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.Write("An error occured");

            }
            return result;


        }

        public async Task<User> UpdateUser(User _obj)
        {

            DateTime DateTime_ = DateTime.Now;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    _obj.LastLogin = DateTime_;
                    _users.Update(_obj);
                    await _context.SaveChangesAsync();
                    transaction.CommitAsync();
                }


                catch (Exception ex)
                {


                    Console.Write("An error occured");
                }
            }
            return _obj;
        }



        public async Task<Product> DeleteProduct(int id)
        {

            DateTime DateTime_ = DateTime.Now;
            var result = new Product();   
            try
            {

                result = _products.Where(m => m.Id == id).First();
                result = _products.Remove(result).Entity;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.Write("An error occured");

            }
            return result;


        }










    }
}
































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































