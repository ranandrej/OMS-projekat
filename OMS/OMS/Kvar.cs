using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS
{
    public class Kvar
    {
        public string IdKv;
        public string VrKv;
        public string statusKv;

        public Kvar(string idKv, string vrKv, string statusKv)
        {
            IdKv = idKv;
            VrKv = vrKv;
            this.statusKv = statusKv;
        }

        public Kvar()
        {
        }
    }
}
