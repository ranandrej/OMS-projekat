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
            while (answer != "8")
            {





                Console.WriteLine("\n\n-----------MENI----------------");
                
                Console.WriteLine("1-Unos Kvara");
                Console.WriteLine("2-Prikaz Kvarova (Osnovne info)");
                Console.WriteLine("3-Unos elektricnog elementa");
                Console.WriteLine("4-Prikaz svih elektricnih elemenata");
                Console.WriteLine("5-Svi Kvarovi (Detaljniji opis svakog)");
                Console.WriteLine("6-Kvarovi u datom vremenskom opsegu");
                Console.WriteLine("7-Azuriranje odredjenog kvara");
                Console.WriteLine("8-Izlaz");
                Console.WriteLine("9-Sacuvaj kvarove u excel");
                Console.WriteLine("10-Pojedinacan kvar sa prioritetom");
                Console.WriteLine("Izaberite opciju:");

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
                        kvisp.IspisiOpseg();
                        break;
                    case "7":
                        kvisp.Azuriranje();
                        break;
                    case "9":
                        kvarDAO.SaveExcel();
                        break;
                    case "10":
                        kvisp.IspisKvarPrioritet();
                        break;

                    
                     
                    

                        

                }
            }
            
        }
    }
}
