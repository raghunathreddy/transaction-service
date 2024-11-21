using BookTransactionServices.Model;
using BookTransactionServices.Repository.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTransactionServices.Repository.Implementation
{
    public class BookTransactionRepository : IBookTransactionRepository
    {
        private readonly IPgSqlConnectionFactory _pgsqlConnectionFactory;
        public BookTransactionRepository(IPgSqlConnectionFactory pgsqlConnectionFactory)
        {
            _pgsqlConnectionFactory = pgsqlConnectionFactory;
        }

        public async void AddBookTransaction(BookTransaction bookTransaction)
        {
            using (IDbConnection connection = _pgsqlConnectionFactory.GetConnection)
            {
                string insertQuery = @"INSERT INTO Books ([book_id],[book_Name],[transaction_date],[transaction_status]) VALUES (@book_Id,@book_Name,@transaction_date,@transaction_status)";
                _pgsqlConnectionFactory.OpenConnection(connection);
                try
                {
                    var results = await connection.ExecuteAsync(insertQuery, new
                    {
                       // book_transactionid = bookTransaction.Book_transactionid,
                        book_Id = bookTransaction.Book_Id,
                        book_Name = bookTransaction.Book_Name,
                        transaction_date = bookTransaction.transaction_date,
                        transaction_status = bookTransaction.transaction_status
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString() + "\n" + ex.InnerException.ToString());
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        public async Task<List<BookTransaction>> GetAllBookTransaction()
        {
            using (IDbConnection connection = _pgsqlConnectionFactory.GetConnection)
            {
                _pgsqlConnectionFactory.OpenConnection(connection);
                using (var transaction = _pgsqlConnectionFactory.BeginTransaction(connection))
                {
                    try
                    {
                        var results = await connection.QueryAsync<BookTransaction>("SELECT * from booktransaction", transaction: transaction);
                        return results?.AsList();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new InvalidOperationException(ex.Message);
                    }
                }
            }
        }

       
    }
}
