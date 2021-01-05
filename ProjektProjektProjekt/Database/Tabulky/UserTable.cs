using Oracle.ManagedDataAccess.Client;
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
    public class UziTable
    {
        public static string TABLE_NAME = "uzi";
        public static string SQL_SELECT = "SELECT user_id, username, password,is_admin, is_manager, klub_id FROM uzi Where user_id = :user_id";
        public static string SQL_LOGIN = "SELECT user_id, username, password,is_admin, is_manager, klub_id FROM uzi Where username = :username and password = :password";

        public static string SQL_SELECT_ALL = "SELECT user_id, username, password,is_admin, is_manager, klub_id FROM uzi";
        public static string SQL_INSERT = "INSERT INTO uzi (username, password, is_admin, is_manager, klub_id) VALUES (:username, :password, :is_admin, :is_manager, :klub_id) RETURNING user_id INTO :user_id";
        

        private static Collection<Uzi> Read(OracleDataReader reader)
        {
            Collection<Uzi> uzis = new Collection<Uzi>();

            while (reader.Read())
            {
                Uzi Uzi = new Uzi();
                int i = -1;
                Uzi.user_id = reader.GetInt32(++i);
                Uzi.username = reader.GetString(++i);
                Uzi.password = reader.GetString(++i);
                Uzi.is_admin = reader.GetString(++i) == "1";
                Uzi.is_manager = reader.GetString(++i) == "1";
                Uzi.klub_id = reader.GetInt32(++i);


                uzis.Add(Uzi);
            }
            return uzis;
        }




       




        public static Uzi Select(int user_id)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_SELECT);
            command.Parameters.Add(new OracleParameter(":user_id", OracleDbType.Int32));
            command.Parameters[":user_id"].Value = user_id;
            OracleDataReader reader = db.Select(command);

            Collection<Uzi> uzis = Read(reader);

            Uzi uzi = null;
            if (uzis.Count == 1)
            {
                uzi = uzis[0];
            }

            reader.Close();
            db.Close();
            return uzi;
        }


        public static Uzi Login(string username, string password)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_LOGIN);
            command.Parameters.Add(new OracleParameter(":username", OracleDbType.Varchar2));
            command.Parameters[":username"].Value = username;
            command.Parameters.Add(new OracleParameter(":password", OracleDbType.Varchar2));
            command.Parameters[":password"].Value = password;
            OracleDataReader reader = db.Select(command);

            Collection<Uzi> uzis = Read(reader);

            Uzi uzi = null;
            if (uzis.Count == 1)
            {
                uzi = uzis[0];
            }

            reader.Close();
            db.Close();
            return uzi;
        }



        public static int Insert(Uzi uzi)
        {
            var db = new Db();
            db.Connect();

            OracleCommand command = db.CreateCommand(SQL_INSERT);
            command.Parameters.Add(":user_id", OracleDbType.Int32);
            command.Parameters[":user_id"].Direction = ParameterDirection.ReturnValue;
            PrepareCommand(command, uzi);
            db.ExecuteNonQuery(command);
            Console.WriteLine(command.Parameters[":user_id"].Value.ToString());

            string idString = command.Parameters[":user_id"].Value.ToString();

            int id = Convert.ToInt32(idString);

            db.Close();
            return id;
        }


        private static void PrepareCommand(OracleCommand command, Uzi uzi)
        {
            command.BindByName = true;
            //command.Parameters.AddWithValue(":klub_id", klub.klub_id);
            command.Parameters.AddWithValue(":username", uzi.username);
            command.Parameters.AddWithValue(":password", uzi.password);
            command.Parameters.AddWithValue(":is_admin", uzi.is_admin ? "1" : "0");
            command.Parameters.AddWithValue(":is_manager", uzi.is_manager ? "1" : "0");
            command.Parameters.AddWithValue(":klub_id", uzi.klub_id);
            

            foreach (OracleParameter p in command.Parameters)
            {
                Debug.WriteLine($"{p.ParameterName} = {p.Value}");
            }
        }
    }
}
