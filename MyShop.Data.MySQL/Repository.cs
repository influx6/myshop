using MyShop.Core.DataRepository;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Data.MySQL
{
    public class Repository<T> : IDataRepository<T> where T : BaseType
    {
        internal DataContext context;
        internal DbSet<T> set;

        public Repository(DataContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public void Delete(string id)
        {
            var t = this.Get(id);
            if (this.context.Entry(t).State == EntityState.Detached)
                this.set.Attach(t);

            this.set.Remove(t);
        }

        public T Get(string id)
        {
            return this.set.Find(id);
        }

        public IQueryable<T> List()
        {
            return this.set;
        }

        public void Save(T t)
        {
            this.set.Add(t);
        }

        public void Update(T t)
        {
            this.set.Attach(t);
            this.context.Entry(t).State = EntityState.Modified;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
