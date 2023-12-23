using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Klase
{
    public class Akcija
    {
        public string IdKv_kv;
        public string VrAk;
        public string opis;

        public Akcija(string idKv_kv, string vrAk, string opis)
        {
            IdKv_kv = idKv_kv;
            VrAk = vrAk;
            this.opis = opis;
        }

        public Akcija()
        {
        }
    }
}
