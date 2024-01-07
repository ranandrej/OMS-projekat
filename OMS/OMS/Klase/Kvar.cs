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
        public string opis;
        public string opisPun;
        public int IdEl;
        public double prioritet;

        public Kvar(string idKv, string vrKv, string statusKv,string opis,string opis_pun,int idEl)
        {
            IdKv = idKv;
            VrKv = vrKv;

            this.statusKv = statusKv;
            this.opis = opis;
            this.opisPun = opis_pun;
            this.IdEl=idEl;
            
        }

        public Kvar()
        {
        }
    }
}
