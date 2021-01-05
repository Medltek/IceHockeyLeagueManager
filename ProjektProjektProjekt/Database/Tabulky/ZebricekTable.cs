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
    public class ZebricekTable
    {
        public static string TABLE_NAME = "Zebricek";
        public static string SQL_SELECT_ALL = "SELECT z.zebricek_id, z.zapasy, z.body, z.skore, z.tym_id, z.smazano, t.tym_nazev  FROM zebricek z join tym t on z.tym_id = t.tym_id order by body desc";

        public static string SQL_SELECT = "SELECT zebricek_id, zapasy, body, skore, tym_id, smazano FROM zebricek WHERE zebricek_id = :zebricek_id";
        public static string SQL_INSERT = "INSERT INTO Zebricek (zapasy, body, skore, tym_id, smazano) VALUES (:zapasy, :body, :skore, :tym_id, :smazano) RETURNING zebricek_id INTO :zebricek_id";
        public static string SQL_UPDATE = "UPDATE Zebricek SET zapasy=:zapasy, body=:body, skore=:skore, tym_id=:tym_id, smazano=:smazano WHERE zebricek_id = :zebricek_id";
        public static string SQL_UPDATE_SMAZANO = "UPDATE Zebricek SET smazano='1' WHERE zebricek_id=:zebricek_id";



        private static Collection<Zebricek> Read(OracleDataReader reader)
        {
            Collection<Zebricek> zebricky = new Collection<Zebricek>();

            while (reader.Read())
            {
                Zebricek Zebricek = new Zebricek();
                int i = -1;
                Zebricek.zebricek_id = reader.GetInt32(++i);
                Zebricek.zapasy = reader.GetInt32(++i);
                Zebricek.body = reader.GetInt32(++i);
                Zebricek.skore = reader.GetInt32(++i);
                Zebricek.tym_id = reader.GetInt32(++i);
                Zebricek.smazano = reader.GetString(++i) == "1";
                Zebricek.tym = new Tym()
                {
                    tym_id = Zebricek.tym_id
                };
                try
                {
                    Zebricek.tym.tym_nazev = reader.GetString(++i);
                }
                catch
                { }

                zebricky.Add(Zebricek);
            }
            return zebricky;
        }

        public static int Update(Zebricek zebricek)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, zebricek);
            command.Parameters.AddWithValue(":zebricek_id", zebricek.zebricek_id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }


        public static int Smazat(Zebricek zebricek)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE_SMAZANO);
            PrepareCommand(command, zebricek);
            command.Parameters.AddWithValue(":zebricek_id", zebricek.zebricek_id);
            //command.Parameters.AddWithValue(":smazano", "1");
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }




        public static Zebricek Select(int zebricek_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT);
            command.Parameters.Add(new OracleParameter(":zebricek_id", OracleDbType.Int32));
            command.Parameters[":zebricek_id"].Value = zebricek_id;
            OracleDataReader reader = db.Select(command);

            Collection<Zebricek> zebricky = Read(reader);

            Zebricek zebricek = null;
            if (zebricky.Count == 1)
            {
                zebricek = zebricky[0];
            }

            reader.Close();
            db.Close();
            return zebricek;
        }

        public static Collection<Zebricek> SelectAll()
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL);
           
            OracleDataReader reader = db.Select(command);

            Collection<Zebricek> zebricky = Read(reader);

            

            reader.Close();
            db.Close();
            return zebricky;
        }

        public static int Insert(Zebricek zebricek)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.Add(":zebricek_id", OracleDbType.Int32);
            command.Parameters[":zebricek_id"].Direction = ParameterDirection.ReturnValue;
            PrepareCommand(command, zebricek);
            db.ExecuteNonQuery(command);
            Console.WriteLine(command.Parameters[":zebricek_id"].Value.ToString());

            string idString = command.Parameters[":zebricek_id"].Value.ToString();

            int id = Convert.ToInt32(idString);

            db.Close();
            return id;
        }


        private static void PrepareCommand(OracleCommand command, Zebricek zebricek)
        {
            command.BindByName = true;
            //command.Parameters.AddWithValue(":klub_id", klub.klub_id);
            command.Parameters.AddWithValue(":zapasy", zebricek.zapasy);
            command.Parameters.AddWithValue(":body", zebricek.body);
            command.Parameters.AddWithValue(":skore", zebricek.skore);
            command.Parameters.AddWithValue(":tym_id", zebricek.tym_id);
            command.Parameters.AddWithValue(":smazano", zebricek.smazano ? "1" : "0");

            foreach (OracleParameter p in command.Parameters)
            {
                Debug.WriteLine($"{p.ParameterName} = {p.Value}");
            }
        }
    }
}
