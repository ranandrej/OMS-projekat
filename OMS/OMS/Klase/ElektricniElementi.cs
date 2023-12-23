using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OMS
{
    public class ElektricniElementi
    {
        public int IdEl;
        public string NazivEl;
        public string TipEl;
        public string GeoLokEl;
        public string NapNivoEl;



        public ElektricniElementi(int idEl, string nazivEl, string tipEl, string geolokEl, string napnivoEl)
        {
            IdEl = idEl;
            NazivEl = nazivEl;
            TipEl = tipEl;
            GeoLokEl = geolokEl;
            NapNivoEl = napnivoEl;

        }

        public ElektricniElementi()
        {
        }
    }
}


