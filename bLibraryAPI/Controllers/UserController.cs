using bLibraryAPI.ConnectionManager.DbContextManager;
using bLibraryAPI.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bLibraryAPI.Controllers
{
    public class UserController : Controller
    {
        private DbManager Manager { get; set; }
        public UserController()
        {
            Manager = new DbManager();
        }
        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var result = Manager.GetUsers();
            return Ok(result);
        }
        // GET api/user/610377f4-dfd9-4f22-9ea7-95a5b8a1b3fc
        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            if (Guid.Parse(id) == Guid.Empty)
                return BadRequest();

            var result = Manager.GetUserById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        // PUT api/books/
        [HttpPut]
        public ActionResult<User> Put(User user)
        {
            if (user == null)
                return BadRequest();

            if (Manager.GetUserById(user.Id) == null)
                return NotFound();

            Manager.UpdateUser(user);
            return Ok(user);
        }
    }
}
