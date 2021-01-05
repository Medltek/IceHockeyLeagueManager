using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProjektProjekt.Database
{
    public class Tymy_Zapasy
    {
        public int tym_id { get; set; }
        public Tym tym { get; set; }
        public Zapas zapas { get; set; }
        public int zapas_id { get; set; }

    }
}
