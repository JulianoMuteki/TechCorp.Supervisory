using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAS.Infra
{
    public interface IFactory
    {
        T Obter<T>();
        T ObterRepositorio<T>(object pContexto);
    }
}
