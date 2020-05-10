using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public interface IUnsafeRepository<T>
    {
        int Insert(T item);
        void Update(T item);
        void Delete(int id);
    }
}
