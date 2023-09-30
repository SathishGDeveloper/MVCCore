using Assigment;
using System.Data.SqlClient;

namespace Assigment.Utilities
{
    public class DBConfiguration
    {
        public DBConfiguration()
        {
            InitializeProperties();
        }

        private string _connectionString;
        private string _dbServer;
        private string _database;
        private string _userId;
        private string _password;

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public string DBServer
        {
            get { return _dbServer; }
        }

        public string DataBase
        {
            get { return _database; }
        }

        public string UserId
        {
            get { return _userId; }
        }

        public string Password
        {
            get { return _password; }
        }

        public void InitializeProperties()
        {
            string Constring = Startup.Connectionstring;
            if (Constring != null)
            {
                _connectionString = Constring;
                var _builder = new SqlConnectionStringBuilder(_connectionString);
                _dbServer = _builder.DataSource;
                _database = _builder.InitialCatalog;
            }
            else
            {
                //throw new Constring("Configuration not made");
            }
        }
    }
}
