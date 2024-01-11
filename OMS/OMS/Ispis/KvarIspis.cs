using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DAO;
using OMS.Klase;
using OMS.Services;
using OMS.DTO;
namespace OMS.Ispis
{
    //Klasa za ispis informacija
    public class KvarIspis
    {
        public static KvarDAO kvarDAO = new KvarDAO();
        public static AkcijeDAO akcijaDAO = new AkcijeDAO();
        public static KvarAllInfo kvarService = new KvarAllInfo();//klasa za kompleksni upit - Svi kvarovi sa akcijama i elementima.

        public void IspisiKvarove()
        {
            Console.WriteLine("--------------SVI KVAROVI--------------");

            foreach (KvarAkcijaDTO dto in kvarService.KvarElAkcije())
            {
                Console.WriteLine("-----------KVAR-------------------------------------------------------------------------");
                Console.WriteLine("{0,-25}{1,-20}{2,-15}", "IDKVAR", "VREME_KV", "STATUS");
                Console.WriteLine("{0,-25}{1,-20}{2,-15}\n\n", dto.k.IdKv, dto.k.VrKv, dto.k.statusKv);
                Console.WriteLine("ELEMENT---" + dto.el.NazivEl + "\t" + dto.el.NapNivoEl + "\n\n");
                Console.WriteLine("PREDUZETE AKCIJE-----");
                Console.WriteLine("{0,-25}{1,-35}", "VREMEAK", "OPIS");
                foreach (Akcija a in dto.akcije)
                {

                    Console.WriteLine("{0,-25}{1,-35}", a.VrAk, a.opis);
                }
                Console.WriteLine("-------------------------------------------------------------------------------------------\n\n");
            }
            Console.WriteLine("------------------------------------------");
        }
        public void KvarAkcija()
        {
            Console.WriteLine("-------------SVI KVAROVI---------------");
            Console.WriteLine("{0,-25}{1,-20}{2,-15}{3,-30}{4,-10}", "IDKV", "VRKV", "STATUS", "Kratak opis", "Broj Akcija");
            foreach (KvarAkcijaDTO dto in kvarService.KvarElAkcije())
            {
                Console.WriteLine("{0,-25}{1,-20}{2,-15}{3,-30}{4,-10}", dto.k.IdKv, dto.k.VrKv, dto.k.statusKv, dto.k.opis, dto.akcije.Count());
            }
            Console.WriteLine("------------------------------------------");

        }

        public void IspisiOpseg()
        {
            List<Kvar> kvaroviOps = kvarDAO.KvaroviUOpsegu();
            Console.WriteLine("--------------KVAROVI U UNETOM OPSEGU-----------------");
            Console.WriteLine("{0,-25}{1,-20}{2,-15}{3,-30}", "IDKV", "VRKV", "STATUS", "Kratak opis");
            foreach (Kvar k in kvaroviOps)
            {
                Console.WriteLine("{0,-25}{1,-20}{2,-15}{3,-30}",k.IdKv,k.VrKv,k.statusKv,k.opis);
            }
        }

        public void Azuriranje()
        {
            kvarService.AzurirajKvar();
        }


        public void IspisiJedanKvarPoId()
        {
            bool pronadjen = false;

            Console.WriteLine("Unesite ID kvara koji želite prikazati: ");
            string unetiId = Console.ReadLine();

            Console.WriteLine("--------------POJEDINAČNI KVAR--------------");

            foreach (KvarAkcijaDTO dto in kvarService.KvarElAkcije())
            {
                if (dto.k.IdKv == unetiId)
                {
                    pronadjen = true;

                    Console.WriteLine("------------------KVAR---------------------");
                    Console.WriteLine("{0,-25}{1,-20}{2,-15}", "IDKVAR", "VREME_KV", "STATUS");
                    Console.WriteLine("{0,-25}{1,-20}{2,-15}\n\n", dto.k.IdKv, dto.k.VrKv, dto.k.statusKv);
                    Console.WriteLine("ELEMENT---" + dto.el.NazivEl + "\t" + dto.el.NapNivoEl + "\n\n");
                    Console.WriteLine("PREDUZETE AKCIJE-----");
                    Console.WriteLine("{0,-25}{1,-35}", "VREMEAK", "OPIS");
                    foreach (Akcija a in dto.akcije)
                    {
                        Console.WriteLine("{0,-25}{1,-35}", a.VrAk, a.opis);
                    }
                    Console.WriteLine("-------------------------------------------------------------------------------------------\n\n");

                    break; // Prekidamo petlju nakon što pronađemo traženi kvar
                }
            }

            if (!pronadjen)
            {
                Console.WriteLine("Kvar sa unetim ID-om nije pronađen.");
            }

            Console.WriteLine("------------------------------------------");
        }



        public void IspisKvarPrioritet()
        {
            Console.WriteLine("Unesite id zeljenog kvara:");
            string id = Console.ReadLine();
            KvarPrioritetDTO dto = kvarService.kvarSaPrioritetom(id);
            Console.WriteLine("----------------KVAR---------------");
            Console.WriteLine("IDKV:" + dto.k.IdKv +"\tOPIS:" + dto.k.opis +"\tELEMENT:" + dto.el.NazivEl+"\tPRIORITET:" + dto.prioritet);
            foreach(Akcija a in dto.akcije)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("OPIS AKCIJE:" + a.opis + "VREME:" + a.VrAk);
            }
        }
    }
}
