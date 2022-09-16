using bLibraryAPI.ConnectionManager.DbContextManager;
using bLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace bLibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private DbManager Manager { get; set; }
        public GenreController()
        {
            Manager = new DbManager();
        }

        // GET api/genre
        [HttpGet]
        public ActionResult<IEnumerable<Genre>> Get()
        {
            var result = Manager.GetElements<Genre>();
            return Ok(result);
        }

        // GET api/genre/610377f4-dfd9-4f22-9ea7-95a5b8a1b3fc
        [HttpGet("{id}")]
        public ActionResult<Genre> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var result = Manager.GetElementById<Genre>(id);
            return Ok(result);
        }

        // POST api/genre
        [HttpPost]
        public ActionResult<Genre> Post(Genre genre)
        {
            if (genre == null)
                return BadRequest();

            Manager.Create<Genre>(genre);
            return Ok(genre);
        }

        // PUT api/genre
        [HttpPut]
        public ActionResult<Genre> Put(Genre genre)
        {
            if (genre == null)
                return BadRequest();

            if (Manager.GetElementById<Genre>(genre.Id) == null)
                return NotFound();

            Manager.Update<Genre>(genre);
            return Ok(genre);
        }

        // DELETE api/genre/610377f4-dfd9-4f22-9ea7-95a5b8a1b3fc
        [HttpDelete("{id}")]
        public ActionResult<Genre> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var result = Manager.GetElementById<Genre>(id);

            if (result == null)
                return NotFound();

            Manager.Delete<Genre>(result);
            return Ok(result);
        }
    }
}
