
using System.Data;


namespace BookTransactionServices.Repository.Interface
{
    public interface IPgSqlConnectionFactory
    {
        IDbConnection GetConnection { get; }
        void OpenConnection(IDbConnection connection);
        IDbTransaction BeginTransaction(IDbConnection connection);
        void ChangeDatabase(IDbConnection connection, string databasename);
    }
}
