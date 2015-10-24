using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;

namespace Fridge.Controllers
{
    public class UsersController : ApiController
    {
      private ICommonRepository<User> data;

      public UsersController(ICommonRepository<User> d)
      {
        data = d;
      }

        // GET: api/Users
        public IEnumerable<User> GetUsers()
        {
          return data.Data;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
          User user = ( from x in data.Data
                        where x.UserID == id
                        select x).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

    // PUT: api/Users/5
    [ResponseType(typeof(void))]
    public IHttpActionResult PutUser(int id, User user)
    {
      user.UserID = id;

      data.SaveData(user);
      return StatusCode(HttpStatusCode.NoContent);
    }

    // POST: api/Users
    [ResponseType(typeof(User))]
    public IHttpActionResult PostUser(User user)
    {

      data.SaveData(user);
      return CreatedAtRoute("DefaultApi", new { id = user.UserID }, user);
    }

    // DELETE: api/Users/5
    [ResponseType(typeof(User))]
    public IHttpActionResult DeleteUser(int id)
    {
      data.DeleteData(id);
      return Ok(id);
    }

    //protected override void Dispose(bool disposing)
    //{
    //    if (disposing)
    //    {
    //        db.Dispose();
    //    }
    //    base.Dispose(disposing);
    //}

    //private bool UserExists(int id)
    //{
    //    return db.Users.Count(e => e.UserID == id) > 0;
    //}
  }
}