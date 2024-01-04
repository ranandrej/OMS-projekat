using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Data;
using OMS.Klase;
using System.Data.SQLite;
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
                Kvar k = new Kvar(reader.GetString(0), reader.GetString(1), reader.GetString(2),reader.GetString(3),reader.GetString(4),reader.GetInt32(5));
                kvarovi.Add(k);
            }
            db.CloseConnection();
            return kvarovi;
        }
        public void UnesiKvar()
        {
            Kvar k = new Kvar();

            Console.WriteLine("Unesite id kvara:");
            k.IdKv = Console.ReadLine();
            Console.WriteLine("Unesite vreme:");
            k.VrKv = Console.ReadLine();
            Console.WriteLine("Unesite status kvara:");
            k.statusKv = Console.ReadLine();
            Console.WriteLine("Kratak opis kvara (do 20 karaktera):");
            k.opis = Console.ReadLine();
            Console.WriteLine("Pun opis kvara:");
           

            k.opisPun = Console.ReadLine();
            Console.WriteLine("Unesite id elementa na kom se desio kvar:");
            k.IdEl = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Unesite broj akcija:");
            int n = Convert.ToInt32(Console.ReadLine());
            DataBase db = new DataBase();
            for (int i=0;i<n;i++)
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
                    Console.WriteLine("Akcija uspesno dodat.");
                };
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

            if (command.ExecuteNonQuery() > 0) {//Ako je promena uspesna ispisi
                Console.WriteLine("Kvar uspesno dodat.");
            };
            db.CloseConnection();
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
                    kvar= new Kvar(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5));

                }

                // Ažuriranje podataka otvorenog kvara (ako nije zatvoren)
                if (kvar.statusKv == "Testiranje" || kvar.statusKv=="U popravci" || kvar.statusKv=="Nepotvrdjen")
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
    }

}
    
