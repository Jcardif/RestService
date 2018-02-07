using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestService.Models;

namespace RestService.Controllers
{
    public class PersonController : ApiController
    {
        // GET: api/Person
        public IEnumerable<string> Get()
        {
            return new string[] { "Person1", "Person2" };
        }

        // GET: api/Person/5
        public Person Get(int id)
        {
            Person p = new Person();
            p.ID = id;
            p.FirstName = "Josh";
            p.LastName = "Cardif";
            p.PayRate = 56.98;
            p.EndDate=DateTime.Parse("2/7/2030");
            p.StartDate=DateTime.Today;

            return p;
        }

        // POST: api/Person
        public HttpResponseMessage Post([FromBody]Person person)
        {
            PersonPersistence pp=new PersonPersistence();
            long Id = pp.SavePerson(person);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location=new Uri(Request.RequestUri, String.Format($"Person/{Id}"));
            return response;
        }

        // PUT: api/Person/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Person/5
        public void Delete(int id)
        {
        }
    }
}
