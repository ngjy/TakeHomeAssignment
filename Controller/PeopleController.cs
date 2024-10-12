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

        [HttpGet("{id}")]
        public IActionResult GetPerson(int id)
        {
            Person person = peopleService.GetPersonById(id);
            if (person == null)
            {
                logger.LogWarning("Person with ID {Id} not found", id);
                return NotFound();
            }

            logger.LogInformation("Returning person data for ID: {Id}", id);
            return Ok(person);
        }

        [HttpPost("add")]
        public IActionResult AddPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid data for person. {@Person}", person);
                return BadRequest(ModelState);
            }

            peopleService.AddPerson(person);
            logger.LogInformation("Person added: {@Person}", person);
            return Ok(person);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            Person person = peopleService.GetPersonById(id);
            if (person == null)
            {
                logger.LogWarning("Invalid, Person with ID {Id} not deleted", id);
                return NotFound();
            }

            peopleService.DeletePerson(id);
            logger.LogInformation("Person deleted with ID: {Id}", id);
            return Ok();
        }
    }
}