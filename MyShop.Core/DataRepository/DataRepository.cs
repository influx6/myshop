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
        IQueryable<T> List();
         T Get(Predicate<T> match);
        void Delete(Predicate<T> match);
        void Update(Predicate<T> match, T t);
    }
}
