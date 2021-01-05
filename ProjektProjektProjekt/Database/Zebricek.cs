using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProjektProjekt.Database
{
    public class Zebricek
    {
        public int zebricek_id { get; set; }
        public int zapasy { get; set; }
        public int body { get; set; }
        public int skore { get; set; }
        public int tym_id { get; set; }

        public Tym tym { get; set; }

        public Boolean smazano { get; set; }
        
    }
}
