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
    public class KlubTable
    {
        public static string TABLE_NAME = "Klub";
        public static string SQL_SELECT_ALL = "SELECT klub_id, klub_nazev, sponzoring, smazano FROM Klub";

        public static string SQL_SELECT = "SELECT klub_id, klub_nazev, sponzoring, smazano FROM Klub WHERE klub_id = :klub_id";
        public static string SQL_INSERT = "INSERT INTO Klub (klub_nazev, sponzoring, smazano) VALUES (:klub_nazev, :sponzoring, :smazano) RETURNING klub_id INTO :klub_id";
        public static string SQL_UPDATE = "UPDATE Klub SET klub_nazev=:klub_nazev, sponzoring=:sponzoring, smazano=:smazano WHERE klub_id=:klub_id";
        public static string SQL_UPDATE_SMAZANO = "UPDATE Klub SET smazano='1' WHERE klub_id=:klub_id";
        public static string SQL_UPDATE_SPONZORING = "PripsaniFinancnichProstredku";
        

        private static Collection<Klub> Read(OracleDataReader reader)
        {
            Collection<Klub> kluby = new Collection<Klub>();

            while (reader.Read())
            {
                Klub Klub = new Klub();
                int i = -1;
                Klub.klub_id = reader.GetInt32(++i);
                Klub.klub_nazev = reader.GetString(++i);
                Klub.sponzoring = reader.GetInt32(++i);
                Klub.smazano = reader.GetString(++i) == "1";


                kluby.Add(Klub);
            }
            return kluby;
        }

        public static int Update(Klub klub)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, klub);
            command.Parameters.AddWithValue(":klub_id", klub.klub_id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }


        public static int Smazat(Klub klub)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE_SMAZANO);
            PrepareCommand(command, klub);
            command.Parameters.AddWithValue(":klub_id", klub.klub_id);
            //command.Parameters.AddWithValue(":smazano", "1");
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }


        public static int Sponzoring(int klub_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommandStoredProcedure(SQL_UPDATE_SPONZORING);
            command.Parameters.Add("sponzoring", OracleDbType.Int32);
            command.Parameters["sponzoring"].Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add(new OracleParameter("p_klub_id", OracleDbType.Int32));
            command.Parameters["p_klub_id"].Value = klub_id;
            db.ExecuteNonQuery(command);
            int result = Convert.ToInt32(command.Parameters["sponzoring"].Value.ToString());

            db.Close();
            return result;
        }

        public static Klub Select(int klub_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT);
            command.Parameters.Add(new OracleParameter(":klub_id", OracleDbType.Int32));
            command.Parameters[":klub_id"].Value = klub_id;
            OracleDataReader reader = db.Select(command);

            Collection<Klub> kluby = Read(reader);

            Klub klub = null;
            if (kluby.Count == 1)
            {
                klub = kluby[0];
            }

            reader.Close();
            db.Close();
            return klub;
        }
        public static Collection<Klub> SelectAll()
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL);
            
            OracleDataReader reader = db.Select(command);

            Collection<Klub> kluby = Read(reader);

            Klub klub = null;
            if (kluby.Count == 1)
            {
                klub = kluby[0];
            }

            reader.Close();
            db.Close();
            return kluby;
        }

        public static int Insert(Klub klub)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.Add(":klub_id", OracleDbType.Int32);
            command.Parameters[":klub_id"].Direction = ParameterDirection.ReturnValue;
            PrepareCommand(command, klub);
            db.ExecuteNonQuery(command);
            Console.WriteLine(command.Parameters[":klub_id"].Value.ToString());

            string idString = command.Parameters[":klub_id"].Value.ToString();

            int id = Convert.ToInt32(idString);

            db.Close();
            return id;
        }


        private static void PrepareCommand(OracleCommand command, Klub klub)
        {
            command.BindByName = true;
            //command.Parameters.AddWithValue(":klub_id", klub.klub_id);
            command.Parameters.AddWithValue(":klub_nazev", klub.klub_nazev);
            command.Parameters.AddWithValue(":sponzoring", klub.sponzoring);
            command.Parameters.AddWithValue(":smazano", klub.smazano ? "1" : "0");

            foreach (OracleParameter p in command.Parameters)
            {
                Debug.WriteLine($"{p.ParameterName} = {p.Value}");
            }
        }
    }
}
