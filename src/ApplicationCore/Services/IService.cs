﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public interface IService<T> : ISafeOperations<T>, IUnsafeOperations<T>
    {
    }
}
