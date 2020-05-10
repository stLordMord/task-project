using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public interface ISafeRepository<T>
    {
        int GetCount(string searchText);
        IList<T> GetAll(int page, int size, string searchText);
        T GetById(int id);
    }
}
