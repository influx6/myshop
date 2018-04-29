using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;
using System.Runtime.Caching;

namespace MyShop.Data.Local
{
    public class ProductRepository
    {
        private static string productCacheName = "products_db";
        private ObjectCache cache = MemoryCache.Default;
        private List<Product> products;

        public ProductRepository()
        {
            products = cache[ProductRepository.productCacheName] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }

        public IQueryable<Product> List()
        {
            return products.AsQueryable();
        }

        public Product Get(String id)
        {
            Product p = this.products.Find((pr) => pr.ID == id);
            if(p == null)
            {
                throw new Exception("not found");
            }
            return p;
        }

        public void Save(Product p)
        {
            this.products.Add(p);
            this.commit();
        }

        public void Update(Product p)
        {
            Product old = this.products.Find((pr) => pr.ID == p.ID);
            if(old == null)
            {
                throw new Exception("not found");
            }

            old = p;
        }

        public void Delete(string id)
        {
            Product old = this.products.Find((pr) => pr.ID == id);
            if(old == null)
            {
                throw new Exception("not found");
            }
            this.products.Remove(old);
        }

        public void commit()
        {
            this.cache[ProductRepository.productCacheName] = this.products;
        }
    }
}
