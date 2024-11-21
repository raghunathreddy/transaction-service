using Service.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBookTransactionService
    {
        public List<DtoBookTransaction> GetAllTransaction();
        public void AddBookTransaction(DtoBookTransaction bookTransaction);
    }
}
