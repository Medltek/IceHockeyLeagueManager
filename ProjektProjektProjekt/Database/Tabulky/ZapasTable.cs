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
    public class ZapasTable
    {
        public static string TABLE_NAME = "Zapas";

        public static string SQL_SELECT = "SELECT zapas_id, goly_tym1, goly_tym2, datum, vyherce_id, vyherce_v_prodlouzeni_id, smazano FROM zapas WHERE zapas_id = :zapas_id";
        
        public static string SQL_INSERT = "INSERT INTO zapas (zapas_id, goly_tym1, goly_tym2, datum, vyherce_id, vyherce_v_prodlouzeni_id, smazano) VALUES (:zapas_id, :goly_tym1, :goly_tym2, :datum, :vyherce_id, :vyherce_v_prodlouzeni_id, :smazano) RETURNING zapas_id INTO :zapas_id";
        //public static string SQL_UPDATE = "UPDATE Zebricek SET zapasy=:zapasy, body=:body,skore=:skore smazano=:smazano WHERE zebricek_id=:zebricek_id";
        public static string SQL_UPDATE_SMAZANO = "UPDATE zapas SET smazano='1' WHERE zapas_id=:zapas_id";



        private static Collection<Zapas> Read(OracleDataReader reader)
        {
            Collection<Zapas> zapasy = new Collection<Zapas>();

            while (reader.Read())
            {
                Zapas Zapas = new Zapas();
                int i = -1;
                Zapas.zapas_id = reader.GetInt32(++i);
                Zapas.goly_tym1 = reader.GetInt32(++i);
                Zapas.goly_tym2 = reader.GetInt32(++i);
                Zapas.datum = reader.GetDateTime(++i);
                Zapas.vyherce_id = reader.GetInt32(++i);
                Zapas.vyherce_v_prodlouzeni_id = reader.GetInt32(++i);
                Zapas.smazano = reader.GetString(++i) == "1";


                zapasy.Add(Zapas);
            }
            return zapasy;
        }

        public static Zapas Select(int zapas_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT);
            command.Parameters.Add(new OracleParameter(":zapas_id", OracleDbType.Int32));
            command.Parameters[":zapas_id"].Value = zapas_id;
            OracleDataReader reader = db.Select(command);

            Collection<Zapas> zapasy = Read(reader);

            Zapas zapas = null;
            if (zapasy.Count == 1)
            {
                zapas = zapasy[0];
            }

            reader.Close();
            db.Close();
            return zapas;
        }
        public static int Smazat(Zapas zapas)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE_SMAZANO);
            PrepareCommand(command, zapas);
            command.Parameters.AddWithValue(":zapas_id", zapas.zapas_id);
            //command.Parameters.AddWithValue(":smazano", "1");
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        /*public static Collection<Zapas> SelectInterval(DateTime a, DateTime b)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT_INTERVAL);
            command.Parameters.Add(new OracleParameter(":tym_id", OracleDbType.Int32));
            command.Parameters[":tym_id"].Value = tym_id;
            OracleDataReader reader = db.Select(command);

            Collection<Zapas> zapasy = Read(reader);

            Zapas zapas = null;
            if (zapasy.Count == 1)
            {
                zapas = zapasy[0];
            }

            reader.Close();
            db.Close();
            return zapasy;
        }*/

       

        public static int Insert(Zapas zapas)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.Add(":zapas_id", OracleDbType.Int32);
            command.Parameters[":zapas_id"].Direction = ParameterDirection.ReturnValue;
            PrepareCommand(command, zapas);
            db.ExecuteNonQuery(command);
            Console.WriteLine(command.Parameters[":zapas_id"].Value.ToString());

            string idString = command.Parameters[":zapas_id"].Value.ToString();

            int id = Convert.ToInt32(idString);

            db.Close();
            return id;
        }


        private static void PrepareCommand(OracleCommand command, Zapas zapas)
        {
            command.BindByName = true;
            command.Parameters.AddWithValue(":zapas_id", zapas.zapas_id);
            command.Parameters.AddWithValue(":goly_tym1", zapas.goly_tym1);
            command.Parameters.AddWithValue(":goly_tym2", zapas.goly_tym2);
            command.Parameters.AddWithValue(":datum", zapas.datum);
            command.Parameters.AddWithValue(":vyherce_id", zapas.vyherce_id);
            command.Parameters.AddWithValue(":vyherce_v_prodlouzeni_id", zapas.vyherce_v_prodlouzeni_id);
            command.Parameters.AddWithValue(":smazano", zapas.smazano ? "1" : "0");


            foreach (OracleParameter p in command.Parameters)
            {
                Debug.WriteLine($"{p.ParameterName} = {p.Value}");
            }
        }
    }
}
