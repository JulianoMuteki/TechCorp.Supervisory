using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAS.Domain.Comum
{
    public interface IRepositorio<T> where T : IEntidade
    {
        /// <summary>
        /// Retornar uma entidade através do campo ID
        /// </summary>
        /// <param name="id">ID do registro da entidade</param>
        /// <returns></returns>
        T ObterPorId(long id);

        //retornar todas as entidades
        IList<T> ListarTodos();

        //salvar ou alterar uma entidade
        bool Salvar(T entity);

        //Excluir uma entidade
        bool Excluir(T entity);

        //Busca pelo proximo ID de acordo com o maior ID salvo no BD + 1.
        long ObterProximoID();

        //DateTime ObterUltimaDataHoraDeAlteracao();
        IList<T> ListarDataUltimaAlteracaoAcimaOuIgualDe(DateTime data);
        IList<T> ListarDataUltimaAlteracaoEntreDatas(DateTime dataInicio, DateTime dataFim);

    }
}
