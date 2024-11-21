using Microsoft.AspNetCore.Mvc;
using Service.DtoModels;
using Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookTransactionServices.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookTransactionController : ControllerBase
    {
        private readonly IBookTransactionService _bookTransactionService;
        public BookTransactionController(IBookTransactionService bookTransactionService)
        {
            _bookTransactionService = bookTransactionService;
        }

        // GET: api/<BookTransactionController>
        [HttpGet]
        public List<DtoBookTransaction> GetAllBookTransaction()
        {
            return _bookTransactionService.GetAllTransaction();
            //return new string[] { "value1", "value2" };
        }

        // GET api/<BookTransactionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BookTransactionController>
        [HttpPost]
        public void AddBookTransaction([FromBody] DtoBookTransaction BookTransaction)
        {
             _bookTransactionService.AddBookTransaction(BookTransaction);
        }

        // PUT api/<BookTransactionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookTransactionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
