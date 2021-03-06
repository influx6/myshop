﻿using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Data.MySQL
{
    public class DataContext : DbContext
    {
        public DataContext()
            :base("DefaultConnection")
        {

        }

        public DbSet<Product> products { get; set; }
        public DbSet<ProductCategory> categories { get; set; }
        public DbSet<BasketItem> items { get; set; }
        public DbSet<Basket> baskets { get; set; }
    }
}
