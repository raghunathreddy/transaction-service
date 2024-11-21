using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookTransactionServices.Repository.Interface;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Core.Configuration;
using Npgsql;


namespace BookTransactionServices.Repository.Implementation
{
    public class PgSqlConnectionFactory : IPgSqlConnectionFactory
    {
        protected readonly IConfiguration _configuration;
        public PgSqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public virtual IDbConnection GetConnection => new NpgsqlConnection(_configuration.GetConnectionString("BookliberaryDB")); //new SqlConnection(_configuration.GetConnectionString("BookliberaryDB"));
        public IDbTransaction BeginTransaction(IDbConnection connection)
        {
            return connection.BeginTransaction();
        }

        public void ChangeDatabase(IDbConnection connection, string databasename)
        {
            connection.ChangeDatabase(databasename);
        }

        public void OpenConnection(IDbConnection connection)
        {
            connection.Open();
        }
    }
}
