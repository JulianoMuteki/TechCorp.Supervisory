using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace ASAS.Infra.RepositoryEF.ModelMap
{
    public class ReleReportMap : EntityTypeConfiguration<ASAS.Domain.Model.Reles.ReleReport>
    {
        public ReleReportMap()
        {
            this.ToTable("Reles");

            HasKey(x => x.Id);
            Property(x => x.DateTimeChange).IsRequired();
            Property(x => x.State).IsRequired();
        }
    }
}
