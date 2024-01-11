using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Klase;
using OMS.Data;
using System.Data.SQLite;
using OMS.DAO.Interfejsi;
namespace OMS.DAO
{
    public class AkcijeDAO:IAkcije
    {
        public List<Akcija> FindAkcije()
        {
            List<Akcija> akcije = new List<Akcija>();
            DataBase db = new DataBase();//Nova instance klase DataBase zbog konekcije
            string query = "select * from Akcije";
            SQLiteCommand command = db.connection.CreateCommand();
            command.CommandText = query;
            db.OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Akcija a = new Akcija(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                akcije.Add(a);
            }
            db.CloseConnection();
            return akcije;
        }
        public List<Akcija> FindAkcijeByKvar(string id)
        {
            List<Akcija> akcije = new List<Akcija>();
            DataBase db = new DataBase();//Nova instance klase DataBase zbog konekcije
            string query = "select * from Akcije where IDKV=@id";
            SQLiteCommand command = db.connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);
            db.OpenConnection();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Akcija a = new Akcija(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                akcije.Add(a);
            }
            db.CloseConnection();
            return akcije;
        }
    }
}
