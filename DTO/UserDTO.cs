﻿using System;

namespace ProductsShop.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
