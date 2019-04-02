using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Filters;
using WebApp.RepositoryPatternImpl;

namespace WebApp.Controllers
{
    [IoCScope]
    public class ValuesController : ApiController
    {

        private IUserService UserService;

        public ValuesController(IUserService userService)
        {
            this.UserService = userService;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var user = UserService.GetByName(name);
                return $"{user.Name} {user.LastName}";
            }
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
