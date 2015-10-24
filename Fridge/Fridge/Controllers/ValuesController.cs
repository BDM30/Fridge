using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fridge.Models.Abstract;
using Fridge.Models.Concrete.Entities;

namespace Fridge.Controllers
{
  public class ValuesController : ApiController
  {

    private ICommonRepository<User> data;

    public ValuesController(ICommonRepository<User> d)
    {
      data = d;
    }
    // GET api/values
    public IEnumerable<string> Get()
    {
      User user = new User() {Name = "Success"};
      data.SaveData(user);

      return new string[] { data.Data.First().Name, "value2" };
    }

    // GET api/values/5
    public string Get(int id)
    {
      return "value";
    }

    // POST api/values
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    public void Delete(int id)
    {
    }
  }
}
