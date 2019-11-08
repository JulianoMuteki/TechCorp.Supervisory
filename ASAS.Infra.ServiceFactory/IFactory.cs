using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAS.Infra.ServiceFactory
{
    public interface IFactory
    {
        T Obter<T>();
        T ObterRepositorio<T>(object pContexto);
    }
}
