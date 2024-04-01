using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsShop.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string? Pcode { get; set; }

        public string PImage { get; set; }
        public int? Price { get; set; }
        public int? MaximumQuantity { get; set; }
       

    }
}   
