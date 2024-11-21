using BookTransactionServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTransactionServices.Repository.Interface
{
    public interface IBookTransactionRepository
    {
       public Task<List<BookTransaction>> GetAllBookTransaction();
       void AddBookTransaction(BookTransaction bookTransaction);
    }
}
