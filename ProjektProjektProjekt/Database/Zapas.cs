using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProjektProjekt.Database
{
    public class Zapas
    {
        public int zapas_id { get; set; }
        public int goly_tym1 { get; set; }
        public int goly_tym2 { get; set; }
        public DateTime datum { get; set; }
        public int vyherce_id { get; set; }
        public int vyherce_v_prodlouzeni_id { get; set; }
        public Boolean smazano { get; set; }
        
    }
}
