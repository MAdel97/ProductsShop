using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProductsShop.Models
{
    public class User
    {
        public User()
        {
        }
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public DateTime LastLogin { get; set; }

    }
}
