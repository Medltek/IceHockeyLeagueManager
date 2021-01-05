using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProjektProjekt.Database
{
    public class Sponzor
    {
        public int sponzor_id { get; set; }
        public string sponzor_nazev{ get; set; }
        public int castka { get; set; }
        public int klub_id { get; set; }
        public Boolean smazano { get; set; }
       
    }
}
