using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication10.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get(string value1,string value2)
        {
           //string value1 = "anu";
           string value3 = DateTime.Now.ToString();
           return new string[] { value1, value2,value3 };
        }
        //public IEnumerable<Client> Get()
        //{

        //    List<Client> clients;
        //    clients = client.Find(emp => true).ToList();
        //    return clients;
        //}

        // GET api/values/5
        public string Get(int id)
        {
            
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value1,string value2)
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
