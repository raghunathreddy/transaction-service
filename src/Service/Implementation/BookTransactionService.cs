using AutoMapper;
using BookTransactionServices.Model;
using BookTransactionServices.Repository.Implementation;
using BookTransactionServices.Repository.Interface;
using Service.DtoModels;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class BookTransactionService : IBookTransactionService
    {
        private readonly IBookTransactionRepository _bookTransactionReposiroty;
        private readonly IMapper _mapper;
        public BookTransactionService(IBookTransactionRepository bookTransactionReposiroty, IMapper mapper)
        {
            _bookTransactionReposiroty = bookTransactionReposiroty;
            _mapper = mapper;
            // _emailHelper = emailHelper;
        }

        public void AddBookTransaction(DtoBookTransaction bookTransaction)
        {
            var transactionhistory = _mapper.Map<BookTransaction>(bookTransaction);
            _bookTransactionReposiroty.AddBookTransaction(transactionhistory);//.Result;
           
        }

        public List<DtoBookTransaction> GetAllTransaction()
        {
            var result = _bookTransactionReposiroty.GetAllBookTransaction().Result;
            return _mapper.Map<List<DtoBookTransaction>>(result);
        }

       
    }
}
