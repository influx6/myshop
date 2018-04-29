using MyShop.Core.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Data.Local
{
    public class Repository<T> : IDataRepository<T>
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

        public T Get(Predicate<T> pr)
        {
            T p = this.models.Find(pr);
            if (p == null)
            {
                throw new Exception("not found");
            }
            return p;
        }

        public void Save(T p)
        {
            this.models.Add(p);
            this.commit();
        }

        public void Update(Predicate<T> pr, T newT)
        {
            T old = this.models.Find(pr);
            if (old == null)
            {
                throw new Exception("not found");
            }

            old = newT;
            this.commit();
        }

        public void Delete(Predicate<T> match)
        {
            T old = this.models.Find(match);
            if (old == null)
            {
                throw new Exception("not found");
            }
            this.models.Remove(old);
            this.commit();
        }

        public void commit()
        {
            this.cache[this.dbName] = this.models;
        }

    }
}
