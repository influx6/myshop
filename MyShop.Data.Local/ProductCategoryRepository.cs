using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Data.Local
{
    public class ProductCategoryRepository
    {
        private static string cacheName = "categories_db";
        private ObjectCache cache = MemoryCache.Default;
        private List<ProductCategory> categories;

        public ProductCategoryRepository()
        {
            categories = cache[ProductCategoryRepository.cacheName] as List<ProductCategory>;
            if(categories == null)
            {
                categories = new List<ProductCategory>();
            }
        }

        public IQueryable<ProductCategory> List()
        {
            return categories.AsQueryable();
        }

        public ProductCategory Get(String id)
        {
            ProductCategory p = this.categories.Find((pr) => pr.ID == id);
            if(p == null)
            {
                throw new Exception("not found");
            }
            return p;
        }

        public void Save(ProductCategory p)
        {
            this.categories.Add(p);
            this.commit();
        }

        public void Update(ProductCategory p)
        {
            ProductCategory old = this.categories.Find((pr) => pr.ID == p.ID);
            if(old == null)
            {
                throw new Exception("not found");
            }

            old = p;
            this.commit();
        }

        public void Delete(string id)
        {
            ProductCategory old = this.categories.Find((pr) => pr.ID == id);
            if(old == null)
            {
                throw new Exception("not found");
            }
            this.categories.Remove(old);
            this.commit();
        }

        public void commit()
        {
            this.cache[ProductCategoryRepository.cacheName] = this.categories;
        }
    }
}
