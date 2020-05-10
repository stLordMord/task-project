using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public interface IUnsafeOperations<T>
    {
        int Insert(T objBLO);
        void Update(T objBLO);
        void Delete(int id);
    }
}
