using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IConverter<Tin, Tout>
    {
        IList<Tout> Convert(IList<Tin> listDTO);
        Tout Convert(Tin objDTO);
        Tin Convert(Tout objBLO);
    }
}
