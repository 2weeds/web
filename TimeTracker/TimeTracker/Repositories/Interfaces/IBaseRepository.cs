using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Repositories.Interfaces
{
    public interface IBaseRepository<T, K>
    {
        IEnumerable<T> GetAll();
        T Get(K id);
        K Add(T model);
        bool Remove(K id);
        bool Exists(K id);
        K Update(T model);
    }
}
