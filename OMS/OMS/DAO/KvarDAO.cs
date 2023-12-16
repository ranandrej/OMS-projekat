using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Data;
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
                Kvar k = new Kvar(reader.GetString(0), reader.GetString(1), reader.GetString(2));
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

            DataBase db = new DataBase();
            string query = "insert into OMS values(@idk,@vrkv,@status)";//Tabela OMS sadrzi informacije o kvarovima
            SQLiteCommand command = new SQLiteCommand(query, db.connection);//Kreiranje SQL komande
            command.Parameters.AddWithValue("@idk", k.IdKv);//Postavljanje vrednosti parametara
            command.Parameters.AddWithValue("@vrkv", k.VrKv);
            command.Parameters.AddWithValue("@status", k.statusKv);
            db.OpenConnection();

            if (command.ExecuteNonQuery() > 0) {//Ako je promena uspesna ispisi
                Console.WriteLine("Kvar uspesno dodat.");
            };
            db.CloseConnection();
        }
    }
}
