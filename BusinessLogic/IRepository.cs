using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IRepository<T>
    {
        //void Create(T item);
        List<T> Read();
        void Update(T item);
        void Destroy(T item);
    }
}
