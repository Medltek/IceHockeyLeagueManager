using Oracle.ManagedDataAccess.Client;
using ProjektProjektProjekt.Database;
using ProjektProjektProjekt.Database.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProjektProjekt.Database.Tabulky
{
    public class SponzorTable
    {
        public static string TABLE_NAME = "Sponzor";
        public static string SQL_SELECT = "SELECT sponzor_id, sponzor_nazev, castka,klub_id, smazano FROM sponzor Where sponzor_id = :sponzor_id";
        public static string SQL_SELECT_ALL = "SELECT sponzor_id, sponzor_nazev, castka,klub_id, smazano FROM sponzor";
        public static string SQL_INSERT = "INSERT INTO sponzor (sponzor_nazev, castka, klub_id, smazano) VALUES (:sponzor_nazev, :castka, :klub_id, :smazano) RETURNING sponzor_id INTO :sponzor_id";
        public static string SQL_UPDATE_SMAZANO = "UPDATE sponzor SET smazano='1' WHERE sponzor_id=:sponzor_id";
       

        private static Collection<Sponzor> Read(OracleDataReader reader)
        {
            Collection<Sponzor> sponzori = new Collection<Sponzor>();

            while (reader.Read())
            {
                Sponzor Sponzor = new Sponzor();
                int i = -1;
                Sponzor.sponzor_id = reader.GetInt32(++i);
                Sponzor.sponzor_nazev = reader.GetString(++i);
                Sponzor.castka = reader.GetInt32(++i);
                Sponzor.klub_id = reader.GetInt32(++i);
                Sponzor.smazano = reader.GetString(++i) == "1";


                sponzori.Add(Sponzor);
            }
            return sponzori;
        }

        


        public static int Smazat(Sponzor sponzor)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE_SMAZANO);
            PrepareCommand(command, sponzor);
            command.Parameters.AddWithValue(":sponzor_id", sponzor.sponzor_id);
            //command.Parameters.AddWithValue(":smazano", "1");
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }


        

        public static Sponzor Select(int sponzor_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT);
            command.Parameters.Add(new OracleParameter(":sponzor_id", OracleDbType.Int32));
            command.Parameters[":sponzor_id"].Value = sponzor_id;
            OracleDataReader reader = db.Select(command);

            Collection<Sponzor> sponzori = Read(reader);

            Sponzor sponzor = null;
            if (sponzori.Count == 1)
            {
                sponzor = sponzori[0];
            }

            reader.Close();
            db.Close();
            return sponzor;
        }

        public static Collection<Sponzor> SelectAll()
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL);
            OracleDataReader reader = db.Select(command);

            Collection<Sponzor> sponzori = Read(reader);

            Sponzor sponzor = null;
            if (sponzori.Count == 1)
            {
                sponzor = sponzori[0];
            }

            reader.Close();
            db.Close();
            return sponzori;
        }

        public static int Insert(Sponzor sponzor)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.Add(":sponzor_id", OracleDbType.Int32);
            command.Parameters[":sponzor_id"].Direction = ParameterDirection.ReturnValue;
            PrepareCommand(command, sponzor);
            db.ExecuteNonQuery(command);
            Console.WriteLine(command.Parameters[":sponzor_id"].Value.ToString());

            string idString = command.Parameters[":sponzor_id"].Value.ToString();

            int id = Convert.ToInt32(idString);

            db.Close();
            return id;
        }


        private static void PrepareCommand(OracleCommand command, Sponzor sponzor)
        {
            command.BindByName = true;
            //command.Parameters.AddWithValue(":klub_id", klub.klub_id);
            command.Parameters.AddWithValue(":sponzor_nazev", sponzor.sponzor_nazev);
            command.Parameters.AddWithValue(":castka", sponzor.castka);
            command.Parameters.AddWithValue(":klub_id", sponzor.klub_id);
            command.Parameters.AddWithValue(":smazano", sponzor.smazano ? "1" : "0");

            foreach (OracleParameter p in command.Parameters)
            {
                Debug.WriteLine($"{p.ParameterName} = {p.Value}");
            }
        }
    }
}
