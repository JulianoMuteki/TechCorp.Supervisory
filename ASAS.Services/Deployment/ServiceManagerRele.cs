using ASAS.Domain.Comum;
using ASAS.Domain.Model.Reles;
using ASAS.Domain.Services;
using ASAS.Infra;
using ASAS.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace ASAS.Services.Deployment
{
   public class ServiceManagerRele: IServiceManagerRele
    {
        public void CreateReportRele()
        {
            Logger.Instancia.Trace(this, "CriarNovoUsuario", "Criando novo Usuario, nome:");
            IGerenciadorDeBD contexto = RepositoryFactory.Instancia.Obter<IGerenciadorDeBD>();

            using (TransactionScope transacao = new TransactionScope())
            {
                try
                {
                    var releRepository = RepositoryFactory.Instancia.ObterRepositorio<IReleReportRepository>(contexto);

                    ReleReport usuario = ReleReportFactory.NewReleReport();
                    releRepository.Salvar(usuario);

                    transacao.Complete();
                }
                catch (Exception ex)
                {
                    Logger.Instancia.Error(this, "CriarNovoUsuario", ex);
                    throw ex;
                }
            }

        }
    }
}
