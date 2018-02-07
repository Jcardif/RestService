﻿using System;
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
            PersonPersistence pp=new PersonPersistence();
            Person p = pp.GetPerson(id);
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
