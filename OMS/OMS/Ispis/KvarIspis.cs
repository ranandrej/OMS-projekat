using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DAO;
using OMS.Klase;
namespace OMS.Ispis
{
    //Klasa za ispis informacija
    public class KvarIspis
    {
        public static KvarDAO kvarDAO = new KvarDAO();
        public static AkcijeDAO akcijaDAO = new AkcijeDAO();

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
        public void KvarAkcija()
        {
            Console.WriteLine("--------------Kvarovi sa akcijama--------------");
            foreach(Kvar k in kvarDAO.FindKvarovi())
            {
                Console.WriteLine("-----------KVAR----------");
                Console.WriteLine("ID kvara:" + k.IdKv+ " Kratak Opis:" + k.opis + " Vreme kvara:" + k.VrKv);
                Console.WriteLine("----------PREDUZETE AKCIJE-------");
                foreach(Akcija a in akcijaDAO.FindAkcijeByKvar(k.IdKv))
                {
                    Console.WriteLine("VREME AKCIJE:" + a.VrAk);
                    Console.WriteLine("OPIS AKCIJE:" + a.opis);
                }
                Console.WriteLine("-------------------------------------");
            }

        }
    }
}
