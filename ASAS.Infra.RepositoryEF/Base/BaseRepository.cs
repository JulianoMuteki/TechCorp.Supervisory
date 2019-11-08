using ASAS.Domain.Comum;
using ASAS.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAS.Infra.RepositoryEF.Base
{
    public abstract class BaseRepository<T> : IRepositorio<T> where T : class, IEntidade
    {
        protected Context _context;

        public BaseRepository()
        {
            throw new Exception("Classe de Repositorio nao pode ser inicializada sem passar o parametro CONTEXTO");

        }

        public BaseRepository(Context context)
        {
            _context = context;
        }

        public IList<T> ListarDataUltimaAlteracaoEntreDatas(DateTime dataInicio, DateTime dataFim)
        {
            Logger.Instancia.Trace(this, "ListarPorDataUltimaAlteracao", "Listando por Data Inicio e Fim");
            var result = _context.Set<T>().Where(t => t.DataHoraUltimaAlteracao >= dataInicio && t.DataHoraUltimaAlteracao <= dataFim);

            if (result == null)
            {
                throw new Exception();// NaoLocalizadoException<IRepositorio<T>>(this, dataInicio, "ListarDataUltimaAlteracaoEntreDatas");
            }
            return result.ToList();
        }

        public IList<T> ListarDataUltimaAlteracaoAcimaOuIgualDe(DateTime data)
        {
            Logger.Instancia.Trace(this, "ListarDataUltimaAlteracaoAcimaOuIgualDe", "Listando por Data Maior ou Igual");
            var result = _context.Set<T>().Where(t => t.DataHoraUltimaAlteracao >= data);

            if (result == null)
            {
                throw new Exception();// throw new NaoLocalizadoException<IRepositorio<T>>(this, data, "ListarDataUltimaAlteracaoAcimaOuIgualDe");
            }
            return result.ToList();
        }

        public T ObterPorId(long id)
        {
            Logger.Instancia.Trace(this, "ObterPorId", "Obtendo por Id na classe base.");

            var result = _context.Set<T>().FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                throw new Exception();//throw new NaoLocalizadoException<IRepositorio<T>>(this, id, "ObterPorLongId");
            }
            return result;
        }

        //public T ObterPorGuid(Guid guid)
        //{
        //    Logger.Instancia.Trace(this, "ObterPorGuid", "Obtendo por Guid na classe base.");

        //    var result = _context.Set<T>().FirstOrDefault(x => x.Guid == guid);
        //    if (result == null)
        //    {
        //        throw new Exception();//  throw new NaoLocalizadoException<IRepositorio<T>>(this, guid, "ObterPorLongGuid");
        //    }
        //    return result;
        //}

        public IList<T> ListarTodos()
        {
            Logger.Instancia.Trace(this, "ListarTodos", "Listando todos <T>.");

            var result = _context.Set<T>().ToList();
            if (result == null)
            {
                throw new Exception();//  throw new NaoLocalizadoException<IRepositorio<T>>(this, 0, "ListarTodos");
            }
            return result;
        }

        public bool Salvar(T entity)
        {
            try
            {
                if (entity.Id == 0L)
                {
                    Logger.Instancia.Trace(this, "Salvar", "Salvando .Add" + entity);

                    _context.Set<T>().Add(entity);
                }
                else
                {
                    Logger.Instancia.Trace(this, "Salvar", "Salvando .Attach" + entity);

                    //deve-se manter essa ordem na execução de um update, caso contrário o EF não compreende que foi realizada uma alteração.
                    //ele acaba passando sem dar erro, mas não salva no BD
                    _context.Set<T>().Attach(entity);
                    _context.Entry<T>(entity).State = System.Data.EntityState.Modified;

                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Instancia.Trace(this, "Salvar", "Não pode incluir " + entity + "." + ex.Message);

                throw new Exception("Não pode incluir " + entity + "." + ex.Message);
            }
        }

        public bool Excluir(T entity)
        {
            try
            {
                Logger.Instancia.Trace(this, "Excluir", "Excluindo " + entity);

                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public long ObterProximoID()
        {
            Logger.Instancia.Trace(this, "ObterProximoID", "Obtendo proximo ID");

            var result = _context.Set<T>().ToList().LastOrDefault<T>();
            if (result == null)
            {
                throw new Exception();//  throw new NaoLocalizadoException<IRepositorio<T>>(this, result.Id, "ObterPorLongId");
            }
            long id = result.Id;
            return id + 1;
        }
    }
}
