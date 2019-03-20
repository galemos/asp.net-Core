using asp.netCoreREST.Model;
using Microsoft.AspNetCore.Mvc;
using asp.netCoreREST.Business;

namespace asp.netCoreREST.Controllers {

    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonsController : ControllerBase
    {
        private IPersonBusiness _personBusiness;

        public PersonsController(IPersonBusiness personBusiness) {
            _personBusiness = personBusiness;
        }

        // GET api/persons/v1
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        // GET api/persons/v1/0
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null)
                return NotFound();
            return Ok(person);
        }

        // POST api/persons/v1
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null)
                return BadRequest();
            return new ObjectResult(_personBusiness.Create(person));
        }

        // PUT api/persons/v1/0
        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null)
                return BadRequest();
            var updatePerson = _personBusiness.Update(person);
            if (updatePerson == null)
                return NoContent();
            return new ObjectResult(updatePerson);
        }

        // DELETE api/persons/v1/0
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
