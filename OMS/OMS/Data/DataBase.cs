using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
namespace OMS.Data
{
    //Klasa za povezivanje sa SQLite bazom
    class DataBase
    {
        public SQLiteConnection connection;
        public DataBase()
        {
            connection = new SQLiteConnection("Data Source=database.sqlite3");//Kreiranje Konekcije
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");//Kreiranje DB fajla ukoliko vec ne postoji
                Console.WriteLine("File created successfuly");
            }
        }
        public void OpenConnection() //Metoda za otvaranje konekcije
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
        }
        public void CloseConnection() // Metoda za zatvaranje konekcije
        {
            if(connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}
