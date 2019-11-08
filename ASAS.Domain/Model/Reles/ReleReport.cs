using ASAS.Domain.Comum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAS.Domain.Model.Reles
{
    [Serializable]
    public class ReleReport : EntidadeBase
    {
        public DateTime DateTimeChange { get; set; }
        public bool State { get; set; }
        public DateTime DataHora1 { get; set; }
        public DateTime DataHora2 { get; set; }

        public DateTime DataHora3 { get; set; }

        public DateTime DataHora4 { get; set; }

        public DateTime DataHora5 { get; set; }

        public DateTime DataHora6 { get; set; }

        public DateTime DataHora7 { get; set; }

        public DateTime DataHora8 { get; set; }

        public DateTime DataHora9 { get; set; }


        internal ReleReport()
            : base()
        {
            this.DateTimeChange = DateTime.Now;
            this.State = true;
            this.DataHora1 = DateTime.Now.AddDays(1);
            this.DataHora2 = DateTime.Now.AddHours(3);
            this.DataHora3 = DateTime.Now.AddMilliseconds(300);
            this.DataHora4 = DateTime.Now.AddSeconds(25);
            this.DataHora5 = DateTime.Now;
            this.DataHora6 = DateTime.Now;
            this.DataHora7 = DateTime.Now;
            this.DataHora8 = DateTime.Now;
            this.DataHora9 = DateTime.Now;
        }
    }
}
