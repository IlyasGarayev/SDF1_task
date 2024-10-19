using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdf.Data.Interface
{
    public interface IRepository<T> where T : class 
    {
        List<T> GetAll();
        void Add(T item);
        void Update(List<T> item);
    }
}
