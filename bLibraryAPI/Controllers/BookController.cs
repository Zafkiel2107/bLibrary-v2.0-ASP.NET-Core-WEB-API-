using bLibraryAPI.ConnectionManager.DbContextManager;
using bLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace bLibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private DbManager Manager { get; set; }
        public BookController()
        {
            Manager = new DbManager();
        }

        // GET api/book
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var result = Manager.GetElements<Book>();
            return Ok(result);
        }

        // GET api/book/610377f4-dfd9-4f22-9ea7-95a5b8a1b3fc
        [HttpGet("{id}")]
        public ActionResult<Book> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var result = Manager.GetElementById<Book>(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST api/books
        [HttpPost]
        public ActionResult<Book> Post(Book book)
        {
            if (book == null)
                return BadRequest();

            Manager.Create<Book>(book);
            return Ok(book);
        }

        // PUT api/books/
        [HttpPut]
        public ActionResult<Book> Put(Book book)
        {
            if (book == null)
                return BadRequest();

            if (Manager.GetElementById<Book>(book.Id) == null)
                return NotFound();

            Manager.Update<Book>(book);
            return Ok(book);
        }

        // DELETE api/books/610377f4-dfd9-4f22-9ea7-95a5b8a1b3fc
        [HttpDelete("{id}")]
        public ActionResult<Book> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var result = Manager.GetElementById<Book>(id);

            if (result == null)
                return NotFound();

            Manager.Delete<Book>(result);
            return Ok(result);
        }
    }
}
