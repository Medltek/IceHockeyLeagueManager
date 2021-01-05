using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProjektProjekt.Database
{
    public class Klub
    {
        public int klub_id { get; set; }
        public string klub_nazev { get; set; }
        public int sponzoring { get; set; }
        public bool smazano { get; set; }

        public override string ToString()
        {
            return "klub_id: {klub_id}, klub_nazev: {klub_nazev}, sponzoring: {sponzoring}, smazano: {smazano}";
        }
    }
}
