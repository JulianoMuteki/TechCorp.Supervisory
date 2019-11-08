using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAS.Domain.Comum
{
    public interface IGerenciadorDeBD
    {
        void CriarBancoDeDados(bool apagarExistente);
        string RetornarStringConexao();
    }
}
