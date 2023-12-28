using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using OMS.Data;
using OMS.DAO;
using OMS.Ispis;
namespace OMS
{
    class Program
    {
        //Pozivanje Metoda za Kvar klasu
        static void Main(string[] args)
        {
            KvarDAO kvarDAO = new KvarDAO();
            KvarIspis kvisp = new KvarIspis();
            ElektricniElementiDAO elementiDao = new ElektricniElementiDAO();
            ElektricniElementiIspis eeisp = new ElektricniElementiIspis();

            string answer = "";
            while (answer != "7")
            {
                

            
            
            

                Console.WriteLine("Izaberite opciju:");
                Console.WriteLine("1-Unos Kvara");
                Console.WriteLine("2-Prikaz Kvarova (Osnovne info)");
                Console.WriteLine("3-Unos elektricnog elementa");
                Console.WriteLine("4-Prikaz svih elektricnih elemenata");
                Console.WriteLine("5-Svi Kvarovi (Detaljniji opis svakog)");
                Console.WriteLine("6-Azuriranje Kvara (Detaljniji opis svakog)");
                Console.WriteLine("7-Izlaz");
                

                answer = Console.ReadLine();


                switch (answer)
                {
                    case "1":
                        kvarDAO.UnesiKvar();
                        break;
                    case "2":
                        kvisp.KvarAkcija();
                        break;
                    case "3":
                        elementiDao.UnesiElektricniElement();

                        break;
                    case "4":
                        eeisp.IspisiElemente();
                        break;
                    case "5":
                        kvisp.IspisiKvarove();
                        break;
                    case "6":
                        kvisp.Azuriranje();
                        break;







                }
            }
            
        }
    }
}
