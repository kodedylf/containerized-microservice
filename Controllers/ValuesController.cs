using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace postgres_service.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly Repository _repo;
        private readonly ILogger _logger;
        public ValuesController(Repository repository, ILogger<ValuesController> logger)
        {
            _repo = repository;
            _logger = logger;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            _logger.LogInformation("Hello");
            return _repo.Personer;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Person value)
        {
            _repo.Personer.Add(value);
            _repo.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var person = _repo.Personer.FirstOrDefault(p => p.PersonId == id);
            if (person != null) {
                _repo.Personer.Remove(person);
                _repo.SaveChanges();
            }
        }
    }
}
