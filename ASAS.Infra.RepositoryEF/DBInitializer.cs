using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAS.Infra.RepositoryEF
{
    public class DBInitializer : System.Data.Entity.CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            //context.Database.ExecuteSqlCommand("ALTER TABLE Usuarios ADD CONSTRAINT uc_UsuarioGuid UNIQUE(Guid)");
            //context.Database.ExecuteSqlCommand("ALTER TABLE Modulos ADD CONSTRAINT uc_ModuloGuid UNIQUE(Guid)");

            //context.Database.ExecuteSqlCommand("ALTER TABLE LeiturasRFID ADD CONSTRAINT uc_LeituraRFIDGuid UNIQUE(Guid)");

            //context.Database.ExecuteSqlCommand("ALTER TABLE BigBags ADD CONSTRAINT uc_BigBagsCodigoRFID UNIQUE(CodigoRFID)");
            //context.Database.ExecuteSqlCommand("ALTER TABLE BigBags ADD CONSTRAINT uc_BigBagGuid UNIQUE(Guid)");

            //context.Database.ExecuteSqlCommand("ALTER TABLE Empresas ADD CONSTRAINT uc_EmpresaGuid UNIQUE(Guid)");

            //context.Database.ExecuteSqlCommand("ALTER TABLE Expedicoes ADD CONSTRAINT uc_ExpedicaoGuid UNIQUE(Guid)");

            //context.Database.ExecuteSqlCommand("ALTER TABLE HistoricosBigBag ADD CONSTRAINT uc_HistoricoBigBagGuid UNIQUE(Guid)");

            //context.Database.ExecuteSqlCommand("ALTER TABLE Laudos ADD CONSTRAINT uc_LaudoGuid UNIQUE(Guid)");

            //context.Database.ExecuteSqlCommand("ALTER TABLE ProdutosEnvasados ADD CONSTRAINT uc_ProdutoEnvasadoGuid UNIQUE(Guid)");

        }

    }
}
