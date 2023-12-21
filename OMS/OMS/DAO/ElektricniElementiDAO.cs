using OMS.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OMS.DAO
{
    public class ElektricniElementiDAO
    {
        public ElektricniElementiDAO() { }

        public List<ElektricniElementi> PronadjiElemente()
        {
            List<ElektricniElementi> elementi = new List<ElektricniElementi>();
            DataBase db = new DataBase();
            string query = "select * from ElektricniElementi";
            SQLiteCommand command = db.connection.CreateCommand();
            command.CommandText = query;
            db.OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ElektricniElementi ee = new ElektricniElementi(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                elementi.Add(ee);
            }
            db.CloseConnection();
            return elementi;
        }

        public void UnesiElektricniElement()
        {
            ElektricniElementi ee = new ElektricniElementi();



            Console.WriteLine("Unesite id elektricnog elementa:");
            ee.IdEl = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Unesite naziv elementa:");
            ee.NazivEl = Console.ReadLine();
            Console.WriteLine("Unesite tip elementa:");
            ee.TipEl = Console.ReadLine();
            Console.WriteLine("Unesite geografsku lokaciju elementa:");
            ee.GeoLokEl = Console.ReadLine();
            Console.WriteLine("Unesite naponski nivo elementa:");
            ee.NapNivoEl = Console.ReadLine();

            DataBase db = new DataBase();
            string query = "insert into ElektricniElementi values(@IdEl,@NazivEl,@TipEl,@GeoLokEl,@NapNivoEl,@IDKV)";//unos u tabelu
            SQLiteCommand command = new SQLiteCommand(query, db.connection);//Kreiranje SQL komande
            command.Parameters.AddWithValue("@IdEl", ee.IdEl);//Postavljanje vrednosti parametara
            command.Parameters.AddWithValue("@NazivEl", ee.NazivEl);
            command.Parameters.AddWithValue("@TipEl", ee.TipEl);
            command.Parameters.AddWithValue("@GeoLokEl", ee.GeoLokEl);
            command.Parameters.AddWithValue("@NapNivoEl", ee.NapNivoEl);
            command.Parameters.AddWithValue("@IDKV", "20020109103025_22");
            db.OpenConnection();

            if (command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("Elektricni element je uspesno dodat.");
            };
            db.CloseConnection();
        }
    }
}
