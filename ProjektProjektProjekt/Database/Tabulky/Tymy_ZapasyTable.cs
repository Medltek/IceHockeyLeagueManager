using Oracle.ManagedDataAccess.Client;
using ProjektProjektProjekt.Database.Tabulky.DTO;
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
    public class Tymy_ZapasyTable
    {

        public static string SQL_SELECT_NAZEV = "SeznamTymu";
        public static string SQL_INSERT = "INSERT INTO tymy_zapasy (tym_id, zapas_id) VALUES (:tym_id, :zapas_id)";

        public static string SQL_SELECT_INTERVAL = "SeznamInterval";



        public static Collection<ZapasUdaje> SelectNazev(string tym_nazev)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommandStoredProcedure(SQL_SELECT_NAZEV);
            command.Parameters.Add(new OracleParameter()
            {
                OracleDbType = OracleDbType.RefCursor,
                Direction = ParameterDirection.Output
            });
            command.Parameters.Add(new OracleParameter("p_tym_nazev", OracleDbType.Varchar2));
            command.Parameters["p_tym_nazev"].Value = tym_nazev;
            var reader = command.ExecuteReader();

            Collection<ZapasUdaje> zapasy = Read(reader);

            reader.Close();
            db.Close();
            return zapasy;
        }


        public static Collection<ZapasUdaje> SelectInterval(DateTime d, DateTime t)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommandStoredProcedure(SQL_SELECT_INTERVAL);
            command.Parameters.Add(new OracleParameter()
            {
                OracleDbType = OracleDbType.RefCursor,
                Direction = ParameterDirection.Output
            });
            command.Parameters.Add(new OracleParameter("p_d", OracleDbType.TimeStamp));
            command.Parameters["p_d"].Value = d;
            command.Parameters.Add(new OracleParameter("p_t", OracleDbType.TimeStamp));
            command.Parameters["p_t"].Value = t;
            var reader = command.ExecuteReader();

            Collection<ZapasUdaje> zapasy = Read(reader);

            reader.Close();
            db.Close();
            return zapasy;
        }

        public static void Insert(Tymy_Zapasy zapasy)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT);
       
            PrepareCommand(command, zapasy);
            db.ExecuteNonQuery(command);
            

            db.Close();
          
        }

        private static Collection<ZapasUdaje> Read(OracleDataReader reader)
        {
            Collection<ZapasUdaje> zapasyudaje = new Collection<ZapasUdaje>();

            while (reader.Read())
            {
                ZapasUdaje zapasUdaje = new ZapasUdaje();
                int i = -1;
                zapasUdaje.Tym1 = new Tym()
                {
                    tym_id = reader.GetInt32(++i),
                    tym_nazev = reader.GetString(++i)
                };
                zapasUdaje.Tym2 = new Tym()
                {
                    tym_id = reader.GetInt32(++i),
                    tym_nazev = reader.GetString(++i)
                };
                zapasUdaje.GolyTym1 = reader.GetInt32(++i);
                zapasUdaje.GolyTym2 = reader.GetInt32(++i);

                zapasyudaje.Add(zapasUdaje);
            }
            return zapasyudaje;
        }

        private static void PrepareCommand(OracleCommand command, Tymy_Zapasy zapasy)
        {
            command.BindByName = true;
            //command.Parameters.AddWithValue(":klub_id", klub.klub_id);
            command.Parameters.AddWithValue(":tym_id", zapasy.tym_id);
            command.Parameters.AddWithValue(":zapas_id", zapasy.zapas_id);
            

            foreach (OracleParameter p in command.Parameters)
            {
                Debug.WriteLine($"{p.ParameterName} = {p.Value}");
            }
        }

    }
}
