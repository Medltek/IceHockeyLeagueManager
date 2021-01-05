using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;

namespace ProjektProjektProjekt.Database
{
    public class Db
    {
        private OracleConnection Connection { get; set; }
        private OracleTransaction SqlTransaction { get; set; }
        public string Language { get; set; }

        public Db()
        {
            Connection = new OracleConnection();
            Language = "en";
        }

        public bool Connect(string connectionString)
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.ConnectionString = connectionString;
                Connection.Open();
            }

            return true;
        }

        public bool Connect()
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                return Connect(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }

        public void BeginTransaction()
        {
            SqlTransaction = Connection.BeginTransaction(IsolationLevel.Serializable);
        }

        public void EndTransaction()
        {
            SqlTransaction.Commit();
            Close();
        }

        public void Rollback()
        {
            SqlTransaction.Rollback();
        }

        public int ExecuteNonQuery(OracleCommand command)
        {
            int rowNumber = 0;
            try
            {
                rowNumber = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Close();
            }
            return rowNumber;
        }

        public OracleCommand CreateCommand(string strCommand)
        {
            OracleCommand command = new OracleCommand(strCommand, Connection);

            if (SqlTransaction != null)
            {
                command.Transaction = SqlTransaction;
            }
            return command;
        }

        public OracleCommand CreateCommandStoredProcedure(string procedureName)
        {
            OracleCommand command = new OracleCommand(procedureName, Connection)
            {
                CommandType = CommandType.StoredProcedure
            };


            return command;
        }

        public OracleDataReader Select(OracleCommand command)
        {
            OracleDataReader sqlReader = command.ExecuteReader();
            return sqlReader;
        }
    }
}
