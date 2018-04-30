using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.DataRepository
{
    public interface IDataRepository<T> 
    {
        void Save(T t);
        void Commit();
        IQueryable<T> List();
         T Get(string id);
        void Delete(string id);
        void Update(T t);
    }
}
