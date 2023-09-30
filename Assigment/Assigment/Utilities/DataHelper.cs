using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Assigment.Utilities
{
    public class DataHelper
    {
        DBConfiguration _config = new DBConfiguration();

        public SqlConnection BuildConnection()
        {
            return new SqlConnection(_config.ConnectionString);
        }

        public SqlCommand BuildGenericCommand(string commandName)
        {
            SqlCommand _cmd = new SqlCommand(commandName);
            return _cmd;
        }

        public SqlCommand BuildCommand(string commandName)
        {
            SqlCommand _cmd = new SqlCommand(commandName);
            _cmd.CommandType = CommandType.StoredProcedure;
            return _cmd;
        }

        public async Task<DataTable> ExecuteDataTable(SqlCommand _cmd)
        {
            DataTable _dataTable = new DataTable();
            if (_cmd == null)
            {
                throw new ArgumentNullException("_cmd");
            }
            else
            {
                _cmd.Connection = BuildConnection();
                if (_cmd.Connection.State == ConnectionState.Closed)
                {
                    _cmd.Connection.Open();
                }
                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_cmd);
                await Task.Run(() => _dataAdapter.Fill(_dataTable));
                if (_cmd.Connection.State == ConnectionState.Open)
                {
                    _cmd.Connection.Close();
                }
                return _dataTable;
            }
        }

        public async Task<DataSet> ExecuteDataSet(SqlCommand _cmd)
        {
            DataSet _dataSet = new DataSet();
            if (_cmd == null)
            {
                throw new ArgumentNullException("_cmd");
            }
            else
            {
                _cmd.Connection = BuildConnection();
                if (_cmd.Connection.State == ConnectionState.Closed)
                {
                    _cmd.Connection.Open();
                }
                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_cmd);
                await Task.Run(() => _dataAdapter.Fill(_dataSet));
                if (_cmd.Connection.State == ConnectionState.Open)
                {
                    _cmd.Connection.Close();
                }
                return _dataSet;
            }
        }

        public object ExecuteScalar(SqlCommand _cmd)
        {
            _cmd.Connection = BuildConnection();
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            object _value = _cmd.ExecuteScalar();
            if (_cmd.Connection.State == ConnectionState.Open)
            {
                _cmd.Connection.Close();
            }
            return _value;
        }

        public int ExecuteNonQuery(SqlCommand _cmd)
        {
            int _returnValue;
            _cmd.Connection = BuildConnection();
            if (_cmd.Connection.State == ConnectionState.Closed)
            {
                _cmd.Connection.Open();
            }
            _cmd.ExecuteNonQuery();
            if (_cmd.Parameters["@returnValue"].Value != null)
            {
                int.TryParse(_cmd.Parameters["@returnValue"].Value.ToString(), out _returnValue);
            }
            else
            {
                _returnValue = 0;
            }
            if (_cmd.Connection.State == ConnectionState.Open)
            {
                _cmd.Connection.Close();
            }
            return _returnValue;
        }
    }
}

