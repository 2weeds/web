using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Services.Interfaces
{
    public interface IBaseService<M, K>
    {
        IEnumerable<M> GetAll();
        M Get(K id);
        K Add(M model);
        bool Remove(K id);
        bool Exists(K id);
        K Update(M model);
    }
        
}
