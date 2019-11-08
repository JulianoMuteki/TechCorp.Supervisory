using ASAS.Domain.Model.Reles;
using ASAS.Infra.RepositoryEF.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAS.Infra.RepositoryEF.Reles
{
    public class ReleReportRepository : BaseRepository<ASAS.Domain.Model.Reles.ReleReport>, IReleReportRepository
    {
        public ReleReportRepository(Context context)
            :base(context)
        {

        }
    }
}
