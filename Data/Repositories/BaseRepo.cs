using System;
using System.Data.SqlClient;
#if NET_FRAMEWORK
using System.Configuration;
#endif
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using DataMaps;

namespace Data.Repositories
{
    public class BaseRepo : IDisposable
    {
        private string _connectionString;

        static BaseRepo()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new AuthorMap());
                config.AddMap(new BookMap());
                config.ForDommel();
            });
        }

        public BaseRepo()
        {
#if NET_FRAMEWORK
            _connectionString = ConfigurationManager.
                ConnectionStrings["DataContext"].ConnectionString;
#else
            throw new NotImplementedException();
#endif
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        protected SqlConnection Connection { get { return new SqlConnection(_connectionString); } }
    }
}