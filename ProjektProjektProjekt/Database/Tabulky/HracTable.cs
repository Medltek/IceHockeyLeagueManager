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
    public class HracTable
    {
        public static string TABLE_NAME = "Hrac";
        
        public static string SQL_SELECT = "SELECT hrac_id, hrac_jmeno, datum_narozeni, cena, vyska, vaha, zahyb, post, tym_id, smazano FROM hrac WHERE hrac_id = :hrac_id";
        public static string SQL_SELECT_TYM = "SELECT hrac_id, hrac_jmeno, datum_narozeni, cena, vyska, vaha, zahyb, post, tym_id, smazano FROM hrac WHERE tym_id = :tym_id";
        public static string SQL_SELECT_JMENO = "SELECT hrac_id, hrac_jmeno, datum_narozeni, cena, vyska, vaha, zahyb, post, tym_id, smazano FROM hrac WHERE hrac_jmeno = :hrac_jmeno";
        public static string SQL_SELECT_ALL = "SELECT hrac_id, hrac_jmeno, datum_narozeni, cena, vyska, vaha, zahyb, post, tym_id, smazano FROM hrac";

        public static string SQL_INSERT = "INSERT INTO Hrac (hrac_jmeno, datum_narozeni, cena, vyska, vaha, zahyb, post, tym_id, smazano) VALUES (:hrac_jmeno, :datum_narozeni, :cena, :vyska, :vaha, :zahyb, :post, :tym_id, :smazano) RETURNING hrac_id INTO :hrac_id";
        public static string SQL_UPDATE = "UPDATE Zebricek SET zapasy=:zapasy, body=:body,skore=:skore smazano=:smazano WHERE zebricek_id=:zebricek_id";
        public static string SQL_UPDATE_SMAZANO = "UPDATE hrac SET smazano='1' WHERE hrac_id=:hrac_id";
        public static string SQL_UPDATE_OCENIT = "Ocenit";

        public static string SQL_KOUPE_HRACE = "KoupeHrace";
        public static string SQL_VYMENA_HRACE = "VymenaHrace";


        private static Collection<Hrac> Read(OracleDataReader reader)
        {
            Collection<Hrac> hraci = new Collection<Hrac>();

            while (reader.Read())
            {
                Hrac Hrac = new Hrac();
                int i = -1;
                Hrac.hrac_id = reader.GetInt32(++i);
                Hrac.hrac_jmeno = reader.GetString(++i);
                Hrac.datum_narozeni = reader.GetDateTime(++i);
                Hrac.cena = reader.GetInt32(++i);
                Hrac.vyska = reader.GetInt32(++i);
                Hrac.vaha = reader.GetInt32(++i);
                Hrac.zahyb = reader.GetString(++i);
                Hrac.post = reader.GetString(++i);
                Hrac.tym_id = reader.GetInt32(++i);
                Hrac.smazano = reader.GetString(++i) == "1";


                hraci.Add(Hrac);
            }
            return hraci;
        }

        public static void Koupit(int hrac_id, int tym_id, int klub_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommandStoredProcedure(SQL_KOUPE_HRACE);

            command.Parameters.Add(new OracleParameter("p_hrac_id", OracleDbType.Int32));
            command.Parameters["p_hrac_id"].Value = hrac_id;
            command.Parameters.Add(new OracleParameter("p_tym_id", OracleDbType.Int32));
            command.Parameters["p_tym_id"].Value = tym_id;
            command.Parameters.Add(new OracleParameter("p_klub_id", OracleDbType.Int32));
            command.Parameters["p_klub_id"].Value = klub_id;
            db.ExecuteNonQuery(command);



            db.Close();


        }

        public static void Vymenit(int hrac_id1, int hrac_id2)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommandStoredProcedure(SQL_VYMENA_HRACE);

            command.Parameters.Add(new OracleParameter("p_hrac_id1", OracleDbType.Int32));
            command.Parameters["p_hrac_id1"].Value = hrac_id1;
            command.Parameters.Add(new OracleParameter("p_hrac_id2", OracleDbType.Int32));
            command.Parameters["p_hrac_id2"].Value = hrac_id2;
            db.ExecuteNonQuery(command);



            db.Close();


        }

        public static Hrac Select(int hrac_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT);
            command.Parameters.Add(new OracleParameter(":hrac_id", OracleDbType.Int32));
            command.Parameters[":hrac_id"].Value = hrac_id;
            OracleDataReader reader = db.Select(command);

            Collection<Hrac> hraci = Read(reader);

            Hrac hrac = null;
            if (hraci.Count == 1)
            {
                hrac = hraci[0];
            }

            reader.Close();
            db.Close();
            return hrac;
        }

       
        public static int Smazat(Hrac hrac)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_UPDATE_SMAZANO);
            PrepareCommand(command, hrac);
            command.Parameters.AddWithValue(":hrac_id", hrac.hrac_id);
            //command.Parameters.AddWithValue(":smazano", "1");
            int ret = db.ExecuteNonQuery(command);

            db.Close();
            return ret;
        }

        public static void Ocenit()
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommandStoredProcedure(SQL_UPDATE_OCENIT);


            db.ExecuteNonQuery(command);

            db.Close();
           
        }

        public static Collection<Hrac> SelectTym(int tym_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT_TYM);
            command.Parameters.Add(new OracleParameter(":tym_id", OracleDbType.Int32));
            command.Parameters[":tym_id"].Value = tym_id;
            OracleDataReader reader = db.Select(command);

            Collection<Hrac> hraci = Read(reader);

            Hrac hrac = null;
            if (hraci.Count == 1)
            {
                hrac = hraci[0];
            }

            reader.Close();
            db.Close();
            return hraci;
        }

        public static Collection<Hrac> SelectAll()
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT_ALL);
          
            OracleDataReader reader = db.Select(command);

            Collection<Hrac> hraci = Read(reader);

            Hrac hrac = null;
            if (hraci.Count == 1)
            {
                hrac = hraci[0];
            }

            reader.Close();
            db.Close();
            return hraci;
        }

        public static Collection<Hrac> SelectJmeno(string hrac_jmeno)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT_JMENO);
            command.Parameters.Add(new OracleParameter(":hrac_jmeno", OracleDbType.Varchar2));
            command.Parameters[":hrac_jmeno"].Value = hrac_jmeno;
            OracleDataReader reader = db.Select(command);

            Collection<Hrac> hraci = Read(reader);

            Hrac hrac = null;
            if (hraci.Count == 1)
            {
                hrac = hraci[0];
            }

            reader.Close();
            db.Close();
            return hraci;
        }

        public static int Insert(Hrac hrac)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.Add(":hrac_id", OracleDbType.Int32);
            command.Parameters[":hrac_id"].Direction = ParameterDirection.ReturnValue;
            PrepareCommand(command, hrac);
            db.ExecuteNonQuery(command);
            Console.WriteLine(command.Parameters[":hrac_id"].Value.ToString());

            string idString = command.Parameters[":hrac_id"].Value.ToString();

            int id = Convert.ToInt32(idString);

            db.Close();
            return id;
        }


        private static void PrepareCommand(OracleCommand command, Hrac hrac)
        {
            command.BindByName = true;
            command.Parameters.AddWithValue(":hrac_id", hrac.hrac_id);
            command.Parameters.AddWithValue(":hrac_jmeno", hrac.hrac_jmeno);
            command.Parameters.AddWithValue(":datum_narozeni", hrac.datum_narozeni);
            command.Parameters.AddWithValue(":cena", hrac.cena);
            command.Parameters.AddWithValue(":vyska", hrac.vyska);
            command.Parameters.AddWithValue(":vaha", hrac.vaha);
            command.Parameters.AddWithValue(":zahyb", hrac.zahyb);
            command.Parameters.AddWithValue(":post", hrac.post);
            command.Parameters.AddWithValue(":tym_id", hrac.tym_id);
            command.Parameters.AddWithValue(":smazano", hrac.smazano ? "1" : "0");
            

            foreach (OracleParameter p in command.Parameters)
            {
                Debug.WriteLine($"{p.ParameterName} = {p.Value}");
            }
        }
    }
}
