using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ParkingMaster.Services.Services;

namespace ParkingMaster.Manager.App_Logic
{
    public class TestingController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public int Get(string pw)
        {
            PasswordValidationManager pvm = new PasswordValidationManager();
            string hashed = pvm.GetHashedPw(pw);
            return pvm.CheckPassword(hashed);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}