using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProjektProjekt.Database
{
    public class Uzi
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int klub_id { get; set; }
        public bool is_admin { get; set; }
        public bool is_manager { get; set; }
    }
}
