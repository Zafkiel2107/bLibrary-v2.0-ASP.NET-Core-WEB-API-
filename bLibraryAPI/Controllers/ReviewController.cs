using bLibraryAPI.ConnectionManager.DbContextManager;
using bLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace bLibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private DbManager Manager { get; set; }
        public ReviewController()
        {
            Manager = new DbManager();
        }

        // GET api/review
        [HttpGet]
        public ActionResult<IEnumerable<Review>> Get()
        {
            var result = Manager.GetElements<Review>();
            return Ok(result);
        }

        // GET api/review/610377f4-dfd9-4f22-9ea7-95a5b8a1b3fc
        [HttpGet("{id}")]
        public ActionResult<Review> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var result = Manager.GetElementById<Review>(id);
            return Ok(result);
        }

        // POST api/review
        [HttpPost]
        public ActionResult<Review> Post(Review review)
        {
            if (review == null)
                return BadRequest();

            Manager.Create<Review>(review);
            return Ok(review);
        }

        // PUT api/review
        [HttpPut]
        public ActionResult<Review> Put(Review review)
        {
            if (review == null)
                return BadRequest();

            if (Manager.GetElementById<Review>(review.Id) == null)
                return NotFound();

            Manager.Update<Review>(review);
            return Ok(review);
        }

        // DELETE api/review/610377f4-dfd9-4f22-9ea7-95a5b8a1b3fc
        [HttpDelete("{id}")]
        public ActionResult<Review> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var result = Manager.GetElementById<Review>(id);

            if (result == null)
                return NotFound();

            Manager.Delete<Review>(result);
            return Ok(result);
        }
    }
}
