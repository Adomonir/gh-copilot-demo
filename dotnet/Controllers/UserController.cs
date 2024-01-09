using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace dotnet.Controllers
{
    public class UserController : ControllerBase
    {
        // GET: api/user
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            // TODO: Implement logic to get all users
            return new string[] { "user1", "user2", "user3" };
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            // TODO: Implement logic to get user by id
            return "user" + id;
        }

        // POST: api/user
        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            // TODO: Implement logic to create a new user
            return "User created: " + value;
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] string value)
        {
            // TODO: Implement logic to update user by id
            return "User updated: " + value;
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            // TODO: Implement logic to delete user by id
            return "User deleted: " + id;
        }
    }
}
