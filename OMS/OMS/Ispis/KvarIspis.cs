using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DAO;
namespace OMS.Ispis
{
    //Klasa za ispis informacija
    public class KvarIspis
    {
        public static KvarDAO kvarDAO = new KvarDAO();

        public void IspisiKvarove()
        {
            Console.WriteLine("--------------SVI KVAROVI--------------");
            Console.WriteLine("{0,-25}{1,-20}{2,-15}","IDKVAR","VREME_KV","STATUS");
            foreach(Kvar k in kvarDAO.FindKvarovi())//Svi kvarovi u tabeli
            {
                Console.WriteLine("{0,-25}{1,-20}{2,-15}", k.IdKv, k.VrKv, k.statusKv);
            }
            Console.WriteLine("------------------------------------------");
        }
    }
}
