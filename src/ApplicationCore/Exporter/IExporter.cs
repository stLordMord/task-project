using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exporter
{
    public interface IExporter<T>
    {
        byte[] Export();
    }
}
