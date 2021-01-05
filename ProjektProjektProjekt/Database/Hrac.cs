using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProjektProjekt.Database
{
    public class Hrac
    {
        public int hrac_id { get; set; }
        public string hrac_jmeno { get; set; }
        public DateTime datum_narozeni { get; set; }
        public int cena { get; set; }

        public int vyska { get; set; }
        public int vaha { get; set; }
        public string zahyb { get; set; }
        public string post { get; set; }
        public int tym_id { get; set; }
        public Boolean smazano { get; set; }
    }
}
