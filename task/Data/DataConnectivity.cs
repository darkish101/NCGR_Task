using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace task
{
    public class DataConnectivity
    {
        public int CommandTimeout { get; set; }
        public string _connectionString { get; set; }
        public SqlConnection _connection { get; set; }
        protected SqlParameterCollection _parameterCollection;
        protected ArrayList _parameters = new ArrayList();
        public bool AutoCloseConnection { get; set; }

        public SqlTransaction _transaction { get; set; }
        public DataConnectivity()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Db_TaskConnectionString1"].ConnectionString;
        }
        public void Connect()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
            }
            else
            {
                if (_connectionString != String.Empty)
                {
                    StringCollection initKeys = new StringCollection();
                    initKeys.AddRange(new string[] { "ARITHABORT", "ANSI_NULLS", "ANSI_WARNINGS", "ARITHIGNORE", "ANSI_DEFAULTS", "ANSI_NULL_DFLT_OFF", "ANSI_NULL_DFLT_ON", "ANSI_PADDING", "ANSI_WARNINGS" });

                    StringBuilder initStatements = new StringBuilder();
                    StringBuilder connectionString = new StringBuilder();

                    Hashtable attribs = this.ParseConfigString(_connectionString);
                    foreach (string key in attribs.Keys)
                    {
                        if (initKeys.Contains(key.Trim().ToUpper()))
                        {
                            initStatements.AppendFormat("SET {0} {1};", key, attribs[key]);
                        }
                        else if (key.Trim().Length > 0)
                        {
                            connectionString.AppendFormat("{0}={1};", key, attribs[key]);
                        }
                    }

                    _connection = new SqlConnection(connectionString.ToString());
                    _connection.Open();

                    if (initStatements.Length > 0)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandTimeout = this.CommandTimeout;
                        cmd.CommandText = initStatements.ToString();
                        cmd.Connection = _connection;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                }
                else
                {
                    throw new InvalidOperationException("You must set a connection object or specify a connection string before calling Connect.");
                }
            }
        }
        public dynamic ExecuteSql(SqlCommand cmd)
        {
            this.Connect();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            cmd.CommandTimeout = this.CommandTimeout;
            cmd.Connection = _connection;
            if (_transaction != null) cmd.Transaction = _transaction;

            cmd.CommandType = CommandType.Text;
            da.GetFillParameters();

            da.SelectCommand = cmd;

            da.Fill(ds);
            da.Dispose();
            cmd.Dispose();

            if (this.AutoCloseConnection) this.Disconnect();

            return ds;
        }

        public dynamic ExecuteSP(SqlCommand cmd, CommandType commandType = CommandType.StoredProcedure, int timeOutSec = 90)
        {
            this.Connect();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                cmd.CommandTimeout = timeOutSec; 
                cmd.Connection = _connection;
                if (_transaction != null) cmd.Transaction = _transaction;
                cmd.CommandType = commandType;
                this.CopyParameters(cmd);

                da.SelectCommand = cmd;

                da.Fill(ds);

                _parameterCollection = cmd.Parameters;
                da.Dispose();
                cmd.Dispose();

                if (this.AutoCloseConnection) this.Disconnect();

                return ds;
            }
            catch (Exception ex) { return null; }
            finally
            {
                da.Dispose();
                cmd.Dispose();
            }
        }

        public string Stringfiy(dynamic ds)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(ds);
            return JSONString;
        }

        private Hashtable ParseConfigString(string config)
        {
            Hashtable attributes = new Hashtable(10, new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture), new CaseInsensitiveComparer(CultureInfo.InvariantCulture));
            string[] keyValuePairs = config.Split(';');
            for (int i = 0; i < keyValuePairs.Length; i++)
            {
                string[] keyValuePair = keyValuePairs[i].Split('=');
                if (keyValuePair.Length == 2)
                {
                    attributes.Add(keyValuePair[0].Trim(), keyValuePair[1].Trim());
                }
                else
                {
                    attributes.Add(keyValuePairs[i].Trim(), null);
                }
            }

            return attributes;
        }
        private void CopyParameters(SqlCommand command)
        {
            for (int i = 0; i < _parameters.Count; i++)
            {
                command.Parameters.Add(_parameters[i]);
            }
        }
        public SqlParameterCollection Parameters
        {
            get
            {
                return _parameterCollection;
            }
        }
        public void Disconnect()
        {
            if ((_connection != null) && (_connection.State != ConnectionState.Closed))
            {
                _connection.Close();
            }

            if (_connection != null) _connection.Dispose();
            if (_transaction != null) _transaction.Dispose();

            _transaction = null;
            _connection = null;
        }
    }
}