using OMS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Ispis
{
    public class ElektricniElementiIspis
    {
        public static ElektricniElementiDAO elementiDAO = new ElektricniElementiDAO();

        public void IspisiElemente()
        {
            Console.WriteLine("--------------SVI ELEMENTI--------------");
            Console.WriteLine("{0,-25}{1,-20}{2,-15}{3, -10}{4,-15}", "IDElementa", "NAZIV_ELEMENTA", "TIPELEMENTA", "GEOLOKEL", "NAPNIVOEL");
            foreach (ElektricniElementi e in elementiDAO.PronadjiElemente())//
            {
                Console.WriteLine("{0,-25}{1,-20}{2,-15}{3, -10}{4,-15}", e.IdEl, e.NazivEl, e.TipEl, e.GeoLokEl, e.NapNivoEl);
            }
            Console.WriteLine("------------------------------------------");
        }
    }
}
