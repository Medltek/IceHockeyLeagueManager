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
    public class TymTable
    {
        public static string TABLE_NAME = "Tym";
        public static string SQL_SELECT = "SELECT tym_id, tym_nazev, klub_id, smazano FROM Tym WHERE tym_id = :tym_id";
        public static string SQL_SELECT_ALL = "SELECT tym_id, tym_nazev, klub_id, smazano FROM Tym";
        public static string SQL_SELECT_ALL_KLUB = "SELECT tym_id, tym_nazev, klub_id, smazano FROM Tym where klub_id = :klub_id";

        public static string SQL_INSERT = "INSERT INTO Tym (tym_nazev, klub_id, smazano) VALUES (:tym_nazev, :klub_id, :smazano) RETURNING tym_id INTO :tym_id";
        public static string SQL_UPDATE = "UPDATE Tym SET tym_nazev=:tym_nazev, klub_id=:klub_id, smazano=:smazano WHERE tym_id=:tym_id";
        public static string SQL_UPDATE_SMAZANO = "UPDATE Tym SET smazano='1' WHERE tym_id=:tym_id";
        public static string SQL_KOUPE_TYMU = "KoupeTymu";



        private static Collection<Tym> Read(OracleDataReader reader)
        {
            Collection<Tym> tymy = new Collection<Tym>();

            while (reader.Read())
            {
                Tym Tym = new Tym();
                int i = -1;
                Tym.tym_id = reader.GetInt32(++i);
                Tym.tym_nazev = reader.GetString(++i);
                Tym.klub_id = reader.GetInt32(++i); 
                Tym.smazano = reader.GetString(++i) == "1";


                tymy.Add(Tym);
            }
            return tymy;
        }

        public static int Update(Tym tym)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, tym);
            command.Parameters.AddWithValue(":tym_id", tym.tym_id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }


        public static int Smazat(Tym tym)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE_SMAZANO);
            PrepareCommand(command, tym);
            command.Parameters.AddWithValue(":tym_id", tym.tym_id);
            //command.Parameters.AddWithValue(":smazano", "1");
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }


        public static void Koupit(int tym_id, int klub_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommandStoredProcedure(SQL_KOUPE_TYMU);
            
            command.Parameters.Add(new OracleParameter("p_tym_id", OracleDbType.Int32));
            command.Parameters["p_tym_id"].Value = tym_id;
            command.Parameters.Add(new OracleParameter("p_klub_id", OracleDbType.Int32));
            command.Parameters["p_klub_id"].Value = klub_id;
            db.ExecuteNonQuery(command);

            

            db.Close();
            

        }

        public static Tym Select(int tym_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT);
            command.Parameters.Add(new OracleParameter(":tym_id", OracleDbType.Int32));
            command.Parameters[":tym_id"].Value = tym_id;
            OracleDataReader reader = db.Select(command);

            Collection<Tym> tymy = Read(reader);

            Tym tym = null;
            if (tymy.Count == 1)
            {
                tym = tymy[0];
            }

            reader.Close();
            db.Close();
            return tym;
        }

        public static Collection<Tym> SelectAll()
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL);
            
            OracleDataReader reader = db.Select(command);

            Collection<Tym> tymy = Read(reader);

            Tym tym = null;
            if (tymy.Count == 1)
            {
                tym = tymy[0];
            }

            reader.Close();
            db.Close();
            return tymy;
        }

        public static Collection<Tym> SelectKlub(int klub_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL);
            command.Parameters.Add(new OracleParameter(":klub_id", OracleDbType.Int32));
            command.Parameters[":klub_id"].Value = klub_id;
            OracleDataReader reader = db.Select(command);

            Collection<Tym> tymy = Read(reader);

            Tym tym = null;
            if (tymy.Count == 1)
            {
                tym = tymy[0];
            }

            reader.Close();
            db.Close();
            return tymy;
        }

        public static int Insert(Tym tym)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.Add(":tym_id", OracleDbType.Int32);
            command.Parameters[":tym_id"].Direction = ParameterDirection.ReturnValue;
            PrepareCommand(command, tym);
            db.ExecuteNonQuery(command);

            Console.WriteLine(command.Parameters[":tym_id"].Value.ToString());

            string idString = command.Parameters[":tym_id"].Value.ToString();

            int id = Convert.ToInt32(idString);

            db.Close();
            return id;
        }


        private static void PrepareCommand(OracleCommand command, Tym tym)
        {
            command.BindByName = true;
            //command.Parameters.AddWithValue(":klub_id", klub.klub_id);
            command.Parameters.AddWithValue(":tym_nazev", tym.tym_nazev);
            command.Parameters.AddWithValue(":klub_id", tym.klub_id);
            command.Parameters.AddWithValue(":smazano", tym.smazano ? "1" : "0");

            foreach (OracleParameter p in command.Parameters)
            {
                Debug.WriteLine($"{p.ParameterName} = {p.Value}");
            }
        }
    }
}
