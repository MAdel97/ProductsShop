using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace ProductsShop.Models
{
    public class Product
    {
        public Product()
        {
        }
        
        public int Id { get; set; }
        public string Category { get; set; }
        public string Pcode { get; set; }

        public string PImage { get; set; }
        public int? Price { get; set; }
        public int? MaximumQuantity { get; set; }
        public decimal DiscountRate { get; set; }



    }
}
