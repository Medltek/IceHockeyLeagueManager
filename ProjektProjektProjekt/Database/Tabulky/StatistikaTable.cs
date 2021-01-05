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
    public class StatistikaTable
    {
        public static string TABLE_NAME = "Statistika";
        public static string SQL_SELECT = "SELECT stat_id, body, goly, asistence, zapasy, hrac_id, smazano FROM Statistika WHERE stat_id = :stat_id";
        public static string SQL_SELECT_HRAC = "SELECT stat_id, body, goly, asistence, zapasy, hrac_id, smazano FROM Statistika WHERE hrac_id = :hrac_id";
        public static string SQL_INSERT = "INSERT INTO Statistika (body, goly, asistence, zapasy, hrac_id, smazano) VALUES (:body, :goly, :asistence, :zapasy, :hrac_id, :smazano) RETURNING stat_id INTO :stat_id";
        public static string SQL_UPDATE = "UPDATE Statistika SET body=:body, goly=:goly, asistence=:asistence, zapasy=:zapasy, hrac_id=:hrac_id, smazano=:smazano WHERE stat_id=:stat_id";
        public static string SQL_UPDATE_SMAZANO = "UPDATE Statistika SET smazano='1' WHERE stat_id=:stat_id";
       


        private static Collection<Statistika> Read(OracleDataReader reader)
        {
            Collection<Statistika> statistiky = new Collection<Statistika>();

            while (reader.Read())
            {
                Statistika Statistika = new Statistika();
                int i = -1;
                Statistika.stat_id = reader.GetInt32(++i);
                Statistika.body = reader.GetInt32(++i);
                Statistika.goly = reader.GetInt32(++i);
                Statistika.asistence = reader.GetInt32(++i);
                Statistika.zapasy = reader.GetInt32(++i);
                Statistika.hrac_id = reader.GetInt32(++i);
                Statistika.smazano = reader.GetString(++i) == "1";

                statistiky.Add(Statistika);
            }
            return statistiky;
        }

        public static int Update(Statistika statistika)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE);
            PrepareCommand(command, statistika);
            command.Parameters.AddWithValue(":stat_id", statistika.stat_id);
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }


        public static int Smazat(Statistika statistika)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE_SMAZANO);
            PrepareCommand(command, statistika);
            command.Parameters.AddWithValue(":stat_id", statistika.stat_id);
            //command.Parameters.AddWithValue(":smazano", "1");
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }


        

        public static Statistika Select(int stat_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT);
            command.Parameters.Add(new OracleParameter(":stat_id", OracleDbType.Int32));
            command.Parameters[":stat_id"].Value = stat_id;
            OracleDataReader reader = db.Select(command);

            Collection<Statistika> statistiky = Read(reader);

            Statistika statistika = null;
            if (statistiky.Count == 1)
            {
                statistika = statistiky[0];
            }

            reader.Close();
            db.Close();
            return statistika;
        }

        public static Collection<Statistika> SelectHrac(int hrac_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT_HRAC);
            command.Parameters.Add(new OracleParameter(":hrac_id", OracleDbType.Int32));
            command.Parameters[":hrac_id"].Value = hrac_id;
            OracleDataReader reader = db.Select(command);

            Collection<Statistika> statistiky = Read(reader);

            Statistika statistika = null;
            if (statistiky.Count == 1)
            {
                statistika = statistiky[0];
            }

            reader.Close();
            db.Close();
            return statistiky;
        }

        public static int Insert(Statistika statistika)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.Add(":stat_id", OracleDbType.Int32);
            command.Parameters[":stat_id"].Direction = ParameterDirection.ReturnValue;
            PrepareCommand(command, statistika);
            db.ExecuteNonQuery(command);
            Console.WriteLine(command.Parameters[":stat_id"].Value.ToString());

            string idString = command.Parameters[":stat_id"].Value.ToString();

            int id = Convert.ToInt32(idString);

            db.Close();
            return id;
        }


        private static void PrepareCommand(OracleCommand command, Statistika statistika)
        {
            command.BindByName = true;
            //command.Parameters.AddWithValue(":klub_id", klub.klub_id);
            command.Parameters.AddWithValue(":body", statistika.body);
            command.Parameters.AddWithValue(":goly", statistika.goly);
            command.Parameters.AddWithValue(":asistence", statistika.asistence);
            command.Parameters.AddWithValue(":zapasy", statistika.zapasy);
            command.Parameters.AddWithValue(":hrac_id", statistika.hrac_id);
            command.Parameters.AddWithValue(":smazano", statistika.smazano ? "1" : "0");

            foreach (OracleParameter p in command.Parameters)
            {
                Debug.WriteLine($"{p.ParameterName} = {p.Value}");
            }
        }
    }
}
