using System.Configuration;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WPF_LoginForm.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            var config = LoadConfiguration();
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        private IConfigurationRoot LoadConfiguration()
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
