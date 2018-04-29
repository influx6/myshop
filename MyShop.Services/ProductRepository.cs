using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.Data.LocalStore
{
    public class ProductRepository
    {
        private static string ProductCacheName = "products_local_cache";

        private ObjectCache cache = MemoryCache.Default;
        private List<Product> products;

        public ProductRepository() {
           this.products = (cache[ProductRepository.ProductCacheName] as List<Product>);
            if (products == null) {
                products = new List<Product>();
            }
        }

        public IQueryable<Product> List()
        {
            return this.products.AsQueryable();
        }

        public void Delete(string id)
        {
            Product product = this.products.Find((p) => p.ID == id);
            if (product == null) {
                throw new Exception("product with id not found");
            }
            this.products.Remove(product);
        }
        
        public Product Get(string id)
        {
            Product product = this.products.Find((p) => p.ID == id);
            if (product == null) {
                throw new Exception("product with id not found");
            }
            return product;
        }

        public void Update(Product p)
        {
            Product product = this.products.Find((pr) => pr.ID == p.ID);
            if (product == null) {
                throw new Exception("product with id not found");
            }

            product = p;
            this.commit();
        }

        public void Save(Product p)
        {
            this.products.Add(p);
            this.commit();
        }

        public void commit()
        {
            this.cache[ProductCacheName] = this.products;
        }
    }
}
