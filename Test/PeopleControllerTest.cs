using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TakeHomeAssignment.Controller;
using TakeHomeAssignment.Services;

namespace TakeHomeAssignment.Test
{
    [TestClass]
    public class PeopleControllerTests
    {
        private PeopleController peopelController;
        private Mock<IPeopleService> peopleServiceMock;
        private Mock<ILogger<PeopleController>> loggerMock;

        [TestInitialize]
        public void Setup()
        {
            peopleServiceMock = new Mock<IPeopleService>();
            loggerMock = new Mock<ILogger<PeopleController>>();
            peopelController = new PeopleController(peopleServiceMock.Object, loggerMock.Object);
        }

        [TestMethod]
        public void GetPerson_ReturnsOkResult_WhenPersonExists()
        {
            int personId = 1;
            Person person = new Person { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };
            peopleServiceMock.Setup(service => service.GetPersonById(personId)).Returns(person);

            IActionResult result = peopelController.GetPerson(1);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetPerson_ReturnsNotFound_WhenPersonDoesNotExists()
        {
            int personId = 1;
            Person person = new Person { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };
            peopleServiceMock.Setup(service => service.GetPersonById(personId)).Returns(person);

            IActionResult result = peopelController.GetPerson(2);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void AddPerson_ReturnsOkResult_WhenPersonIsValid()
        {
            Person person = new Person { Id = 2, Name = "Jane Doe", Email = "jane@example.com", Phone = "1234567890" };
            peopleServiceMock.Setup(service => service.AddPerson(person)).Verifiable();

            IActionResult result = peopelController.AddPerson(person);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void AddPerson_ReturnsBadRequest_WhenPersonIsInvalid()
        {
            Person person = new Person { Id = 0, Name = "", Email = "invalid-email", Phone = "123" }; // Invalid data
            peopelController.ModelState.AddModelError("Name", "Name is required");
            peopelController.ModelState.AddModelError("Email", "A valid email is required");
            peopelController.ModelState.AddModelError("Phone", "Phone number must be 10 digits");

            IActionResult result = peopelController.AddPerson(person);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

            BadRequestObjectResult badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual(3, ((SerializableError)badRequestResult.Value).Count);
        }

        [TestMethod]
        public void DeletePerson_ReturnsOkResult_WhenPersonExists()
        {
            int personId = 1;
            Person person = new Person { Id = personId, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };
            peopleServiceMock.Setup(service => service.GetPersonById(personId)).Returns(person);
            peopleServiceMock.Setup(service => service.DeletePerson(personId)).Verifiable();

            IActionResult result = peopelController.DeletePerson(personId);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void DeletePerson_ReturnsNotFound_WhenPersonDoesNotExist()
        {
            int personId = 1;
            Person person = new Person { Id = personId, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };
            peopleServiceMock.Setup(service => service.GetPersonById(personId)).Returns(person);

            IActionResult result = peopelController.DeletePerson(2);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
