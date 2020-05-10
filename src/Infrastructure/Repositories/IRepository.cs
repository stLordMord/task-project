using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public interface IRepository<T> : ISafeRepository<T>, IUnsafeRepository<T>
    {
    }
}
