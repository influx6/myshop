using MyShop.Core.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.Data.Local
{
    public class Repository<T> : IDataRepository<T> where T : BaseType
    {
        private string dbName;
        private ObjectCache cache = MemoryCache.Default;
        private List<T> models;

        public Repository(){
            this.dbName = typeof(T).Name;
            this.models = this.cache[this.dbName] as List<T>;
            if(this.models == null){
                this.models = new List<T>();
            }
        }

        public IQueryable<T> List()
        {
            return models.AsQueryable();
        }

        public T Get(string id)
        {
            T p = this.models.Find((pr) => pr.ID == id);
            if (p == null)
            {
                throw new Exception("not found");
            }
            return p;
        }

        public void Save(T p)
        {
            this.models.Add(p);
        }

        public void Update(T newT)
        {
            T old = this.models.Find((pr) => pr.ID == newT.ID);
            if (old == null)
            {
                throw new Exception("not found");
            }

            old = newT;
        }

        public void Delete(string id)
        {
            T old = this.models.Find((p) => p.ID == id);
            if (old == null)
            {
                throw new Exception("not found");
            }
            this.models.Remove(old);
        }

        public void Commit()
        {
            this.cache[this.dbName] = this.models;
        }

    }
}
