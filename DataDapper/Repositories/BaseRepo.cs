using System;
using System.Data.SqlClient;
using System.Configuration;
namespace DataDapper.Repositories
{
    public class BaseRepo : IDisposable
    {
        private string _connectionString;

        public BaseRepo()
        {
            _connectionString = ConfigurationManager.
                ConnectionStrings["DataContext"].ConnectionString;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        protected SqlConnection Connection { get { return new SqlConnection(_connectionString); } }
    }
}