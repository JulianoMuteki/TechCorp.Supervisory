using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using ASAS.Domain.Model.Reles;
using ASAS.Infra.RepositoryEF.Reles;
using ASAS.Domain.Comum;

namespace ASAS.Infra.RepositoryEF
{
    public class ModelEF : NinjectModule
    {
        public override void Load()
        {
            Bind<IGerenciadorDeBD>().To<Context>();

            Bind<IReleReportRepository>().To<ReleReportRepository>();
        }
    }
}
