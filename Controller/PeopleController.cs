using Microsoft.AspNetCore.Mvc;
using TakeHomeAssignment.Services;

namespace TakeHomeAssignment.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService peopleService;
        private readonly ILogger<PeopleController> logger;

        public PeopleController(IPeopleService peopleService, ILogger<PeopleController> logger)
        {
            this.peopleService = peopleService;
            this.logger = logger;
        }

        // Get a person by ID
        [HttpGet("{id}")]
        public IActionResult GetPerson(int id)
        {
            Person person = peopleService.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // Add a new person
        [HttpPost("add")]
        public IActionResult AddPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            peopleService.AddPerson(person);
            logger.LogInformation("Person added: {@Person}", person);
            return Ok(person);
        }

        // Delete a person by ID
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            var person = peopleService.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }
            peopleService.DeletePerson(id);
            logger.LogInformation("Person deleted with ID: {Id}", id);
            return Ok();
        }
    }
}