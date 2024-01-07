using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Data;
using OMS.Klase;
using System.Data.SQLite;
using ClosedXML.Excel;
using OMS.DTO;
namespace OMS.DAO
{
    //Klasa za implementaciju metoda nad kvarovima
    public class KvarDAO
    {
        public KvarDAO()
        {
        }
        public List<Kvar> FindKvarovi()
        {
            List<Kvar> kvarovi = new List<Kvar>();
            DataBase db = new DataBase();//Nova instance klase DataBase zbog konekcije
            string query = "select * from OMS";
            SQLiteCommand command = db.connection.CreateCommand();
            command.CommandText = query;
            db.OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Kvar k = new Kvar(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5));
                kvarovi.Add(k);
            }
            db.CloseConnection();
            return kvarovi;
        }
        public int brKvarZaDatum(DateTime dt)
        {
            string formattedDate = dt.ToShortDateString();

            // Kreiranje SQL upita
            string query = "SELECT COUNT(*) FROM OMS WHERE vrkv = @CurrentDate";


            DataBase db = new DataBase();
            SQLiteCommand komanda = new SQLiteCommand(query, db.connection);
            db.OpenConnection();
            komanda.Parameters.AddWithValue("@CurrentDate", formattedDate);

            int num = Convert.ToInt32(komanda.ExecuteScalar());
            db.CloseConnection();
            Console.WriteLine("Ukupno kvarova danas:" + num);
            return num;
        }
        public void UnesiKvar()
        {
            Kvar k = new Kvar();

            DateTime trenutno = DateTime.Now;
            int rb = brKvarZaDatum(trenutno);
            if (rb <= 9)
            {
                k.IdKv = trenutno.ToString("yyyyMMddhhmmss") + "_0" + (rb + 1);
            }
            else
            {
                k.IdKv = trenutno.ToString("yyyyMMddhhmmss") + "_" + (rb + 1);
            }


            k.VrKv = trenutno.ToString("yyyy-MM-dd");


            Console.WriteLine("Kratak opis kvara (do 20 karaktera):");
            k.opis = Console.ReadLine();
            Console.WriteLine("Pun opis kvara:");


            k.opisPun = Console.ReadLine();
            Console.WriteLine("Unesite id elementa na kom se desio kvar:");
            k.IdEl = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Unesite broj akcija:");
            int n = Convert.ToInt32(Console.ReadLine());
            if (n >= 2)
            {
                k.statusKv = "U popravci";
            }
            else
            {
                k.statusKv = "Nepotvrdjen";
            }
            DataBase db = new DataBase();
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    Akcija a = new Akcija();

                    a.IdKv_kv = k.IdKv;
                    Console.WriteLine("Vreme akcije (dan/mesec/godina-sat)");
                    a.VrAk = Console.ReadLine();
                    Console.WriteLine("Opis akcije:");
                    a.opis = Console.ReadLine();
                    string sql = "insert into Akcije values(@idk,@vrak,@opis)";
                    SQLiteCommand komanda = new SQLiteCommand(sql, db.connection);//Kreiranje SQL komande
                    komanda.Parameters.AddWithValue("@idk", a.IdKv_kv);//Postavljanje vrednosti parametara
                    komanda.Parameters.AddWithValue("@vrak", a.VrAk);
                    komanda.Parameters.AddWithValue("@opis", a.opis);
                    db.OpenConnection();

                    if (komanda.ExecuteNonQuery() > 0)
                    {//Ako je promena uspesna ispisi
                        Console.WriteLine("Akcija uspesno dodata.");
                    };
                }
            }


            string query = "insert into OMS values(@idk,@vrkv,@status,@opis,@opisPun,@idel)";//Tabela OMS sadrzi informacije o kvarovima
            SQLiteCommand command = new SQLiteCommand(query, db.connection);//Kreiranje SQL komande
            command.Parameters.AddWithValue("@idk", k.IdKv);//Postavljanje vrednosti parametara
            command.Parameters.AddWithValue("@vrkv", k.VrKv);
            command.Parameters.AddWithValue("@status", k.statusKv);
            command.Parameters.AddWithValue("@opis", k.opis);
            command.Parameters.AddWithValue("@opisPun", k.opisPun);
            command.Parameters.AddWithValue("@idel", k.IdEl);
            db.OpenConnection();

            if (command.ExecuteNonQuery() > 0)
            {//Ako je promena uspesna ispisi
                Console.WriteLine("Kvar uspesno dodat.");
            };
            db.CloseConnection();
        }

        public List<Kvar> KvaroviUOpsegu()
        {
            List<Kvar> kvarovi = new List<Kvar>();
            DataBase db = new DataBase();//Nova instance klase DataBase zbog konekcije
            Console.Write("Unesite početni datum (YYYY-MM-DD): ");
            string pocetniDatum = Console.ReadLine();
            Console.Write("Unesite završni datum (YYYY-MM-DD): ");
            string zavrsniDatum = Console.ReadLine();
            string query = "select * from OMS where VRKV between @pocetniDatum and @zavrsniDatum";
            SQLiteCommand command = db.connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@pocetniDatum", pocetniDatum);
            command.Parameters.AddWithValue("@zavrsniDatum", zavrsniDatum);
            db.OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            Console.WriteLine("Lista kvarova u zadatom vremenskom opsegu:");
            while (reader.Read())
            {
                Kvar k = new Kvar(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5));
                kvarovi.Add(k);
            }
            db.CloseConnection();
            return kvarovi;
        }






        public Kvar PrikazPoId(int id)
        {
            Kvar kvar = new Kvar();
            DataBase db = new DataBase();
            string query = "select * from OMS where IdKv=@id";
            SQLiteCommand command = db.connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);
            db.OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Kvar k = new Kvar(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5));
            }
            return kvar;
        }



        public void AzurirajKvarove(string id)
        {
            Kvar kvar = new Kvar();
            DataBase db = new DataBase();
            string query = "select * from OMS where IdKv=@id";
            SQLiteCommand command = db.connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);

            db.OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();

            if (kvar != null)
            {
                while (reader.Read())
                {

                    kvar = new Kvar(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5));

                    kvar = new Kvar(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5));


                }

                // Ažuriranje podataka otvorenog kvara (ako nije zatvoren)

                if (kvar.statusKv == "Testiranje" || kvar.statusKv == "U popravci" || kvar.statusKv == "Nepotvrdjen")

                    if (kvar.statusKv == "Testiranje" || kvar.statusKv == "U popravci" || kvar.statusKv == "Nepotvrdjen")
                    {
                        AžurirajPodatkeOtvorenogKvara(kvar);
                    }
                    else
                    {
                        Console.WriteLine("Nije moguće ažurirati podatke zatvorenog kvara.");
                    }
            }
            else
            {
                Console.WriteLine("Kvar sa unetim ID-om nije pronađen.");
            }
        }


        // Metod za ažuriranje podataka otvorenog kvara
        static void AžurirajPodatkeOtvorenogKvara(Kvar kvar)
        {
            DataBase db = new DataBase();

            Console.Write("Unesite  novi status za ažuriranje: ");
            string noviStatus = Console.ReadLine();
            if (!string.IsNullOrEmpty(noviStatus))
            {


                Kvar k = new Kvar();

                string query = "update OMS set statusKv=@noviStatus where idKv=@idKv";
                SQLiteCommand command = db.connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@noviStatus", noviStatus);
                command.Parameters.AddWithValue("@idKv", k.IdKv);
                db.OpenConnection();
                command.ExecuteNonQuery();


            }



            Console.Write("Unesite  novi opis kvara za ažuriranje: ");
            string noviDuziOpis = Console.ReadLine();
            kvar.opisPun = noviDuziOpis;

            if (!string.IsNullOrEmpty(noviDuziOpis))
            {


                Kvar k = new Kvar();

                string query = "update OMS set opisPun=@opisPun where idKv=@idKv";
                SQLiteCommand command = db.connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@opisPun", noviDuziOpis);
                command.Parameters.AddWithValue("@idKv", k.IdKv);




                command.ExecuteNonQuery();


            }

            Console.WriteLine("Unesite broj novih akcija:");
            int n = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Akcija a = new Akcija();

                a.IdKv_kv = kvar.IdKv;
                Console.WriteLine("Vreme akcije (dan/mesec/godina-sat)");
                a.VrAk = Console.ReadLine();
                Console.WriteLine("Opis akcije:");
                a.opis = Console.ReadLine();
                string sql = "insert into Akcije values(@idk,@vrak,@opis)";
                SQLiteCommand komanda = new SQLiteCommand(sql, db.connection);//Kreiranje SQL komande
                komanda.Parameters.AddWithValue("@idk", a.IdKv_kv);//Postavljanje vrednosti parametara
                komanda.Parameters.AddWithValue("@vrak", a.VrAk);
                komanda.Parameters.AddWithValue("@opis", a.opis);

                db.OpenConnection();


                if (komanda.ExecuteNonQuery() > 0)
                {//Ako je promena uspesna ispisi
                    Console.WriteLine("Akcija uspesno dodat.");
                };
            }

            db.CloseConnection();



            Console.WriteLine("Podaci su uspešno ažurirani.");
        }

        public void SaveExcel()
        {
            string query = "select * from OMS";
            DataBase db = new DataBase();
            db.OpenConnection();
            SQLiteCommand command = db.connection.CreateCommand();
            command.CommandText = query;


            SQLiteDataReader reader = command.ExecuteReader();

            var Woorkbook = new XLWorkbook();
            var WorkSheet = Woorkbook.AddWorksheet("KVAROVI");

            for (int i = 0; i < reader.FieldCount; i++)
            {
                WorkSheet.Cell(1, i + 1).Value = reader.GetName(i);
            }
            int rowNumber = 2;
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    WorkSheet.Cell(rowNumber, i + 1).Value = reader[i].ToString();
                }
                rowNumber++;
            }
            Woorkbook.SaveAs("oms.xlsx");
            Console.WriteLine("Excel file exported successfully.");
        }
        // --------------------------------------------------------------------------------------------------------

        public Kvar PrikaziPrioritet(string id)
        {
            Kvar kvar = new Kvar();
            DataBase db = new DataBase();
            string query = "select prioritet from OMS where IdKv=@id";
            SQLiteCommand command = db.connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);
            db.OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                kvar.IdKv = reader.GetString(0);
                kvar.prioritet = reader.GetDouble(6);
            }

            db.CloseConnection();
            return kvar;
        }



        // --------------------- POSTAVLJANJE PRIORITETA KVARA -------------------------
          public Kvar PostaviPrioritetKvara(string idKvara, double prioritet)
          {
              Kvar kvar = new Kvar();
              DataBase db = new DataBase();//Nova instance klase DataBase zbog konekcije

              string query = "UPDATE OMS SET Prioritet = Prioritet + 1 WHERE IdKv=@id AND Status = 'U popravci' AND VrKv < @today";
              SQLiteCommand command = db.connection.CreateCommand();
              command.CommandText = query;
              command.Parameters.AddWithValue("@id", idKvara);
              command.Parameters.AddWithValue("@today", DateTime.Today);

              
              command.ExecuteNonQuery();
              db.CloseConnection();
              return kvar;


          }


          public Akcija AzurirajPrioritetZaAkciju(string idKvara, double noviP)
         {

             double trenutniPrioritet=Convert.ToDouble(PrikaziPrioritet(idKvara));
             Akcija novaAkcija= new Akcija();

             double noviPrioritet=trenutniPrioritet+(Convert.ToDouble(novaAkcija.IdKv_kv)*0.5);
             DataBase db = new DataBase();//Nova instance klase DataBase zbog konekcije
           string query = "UPDATE OMS SET Prioritet = @noviPrioritet WHERE Status = 'U popravci' AND IDKV=@idKvara";
                 SQLiteCommand command = db.connection.CreateCommand();
                 command.CommandText = query;
                 command.Parameters.AddWithValue("@noviPrioritet", noviPrioritet);
                 command.Parameters.AddWithValue("@idKvara", idKvara);

            command.ExecuteNonQuery();

            db.CloseConnection();

            return novaAkcija;

                 }


         }

    }






       

    



