using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Data.MySQL
{
    public class Repository<T>
    {
        public Repository()
        {

        }
    }

    public class DataContext : DbContext
    {
        public DataContext()
            :base("DefaultConnection")
        {

        }

        public DbSet<Product> products { get; set; }
        public DbSet<ProductCategory> categories { get; set; }
    }
}
