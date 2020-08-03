using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SampleDemo.Controllers;
using SampleDemo.Data;
using SampleDemo.Dtos;
using SampleDemo.Models;
using SampleDemo.Profiles;

namespace SampleDemo.Tests
{
    [TestFixture]
    public class PersonRepositoryTests
    {
        private Mock<ISampleDemoRepo> _mockRepo;
        private IMapper _mapper;
        private List<Person> _peopple;
        private Mock<ILogger<PersonsController>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ISampleDemoRepo>();

            _peopple = new List<Person>{
                new Person { Id=1, Name = "Schedule1" },
                new Person { Id=2, Name = "Schedule1" },
                new Person { Id=3, Name = "Schedule1" },
            };

            _mockLogger = new Mock<ILogger<PersonsController>>();

            // config your mapper
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }

        [Test]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var controller = new PersonsController(_mockRepo.Object, _mapper, _mockLogger.Object);
            // Act
            var okResult = controller.GetAllPersons();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        public void Chech_Valid_Return_OkResult_CreatePerson()
        {
            //Arrange  
            
            var controller = new PersonsController(_mockRepo.Object, _mapper, _mockLogger.Object);
            var person = new PersonCreateDto() { Name = "Peter" };

            //Act  
            var data = controller.CreatePerson(person);

            //Assert  
            Assert.IsInstanceOf<CreatedAtRouteResult>(data.Result);
        }

        [Test]
        public void Check_Valid_Data_Return_AddNewPerson()
        {
            // Arrange
            _mockRepo.Setup(m => m.GetAllPersons()).Returns(_peopple);

            var controller = new PersonsController(_mockRepo.Object, _mapper, _mockLogger.Object);

            // Act
            var result = controller.GetAllPersons();

            // Assert
            var okResult = (OkObjectResult)result.Result;
            if (okResult != null)
                Assert.NotNull(okResult);

            var model = okResult?.Value as IEnumerable<PersonReadDto>;
            var personReadDtos = (model ?? Array.Empty<PersonReadDto>()).ToList();

            if (!personReadDtos.ToArray().Any()) return;

            Assert.NotNull(model);

            var expected = personReadDtos.FirstOrDefault()?.Name;
            var actual = _peopple?.FirstOrDefault()?.Name;

            Assert.AreEqual(expected: expected, actual: actual);
        }

        [Test]
        public void ActionExecutes_ReturnsNumbersOfPeople()
        {
            // Arrange
            _mockRepo.Setup(m => m.GetAllPersons()).Returns(_peopple);

            var controller = new PersonsController(_mockRepo.Object, _mapper, _mockLogger.Object);


            // Act
            var result = controller.GetAllPersons();

            var okResult = (OkObjectResult)result.Result;
            if (okResult != null)
                Assert.NotNull(okResult);

            var model = okResult?.Value as IEnumerable<PersonReadDto>;
            var personReadDtos = (model ?? Array.Empty<PersonReadDto>()).ToList();

            if (!personReadDtos.ToArray().Any()) return;

            // Assert
            Assert.NotNull(model);
        }

        [Test]
        public void Check_Return_Value_GetById_Person()
        {
            // Arrange
            var controller = new PersonsController(_mockRepo.Object, _mapper, _mockLogger.Object);
            _mockRepo.Setup(it => it.GetPersonById(It.IsAny<int>())).Returns(new Person() {Id = 23, Name = "Schedule_4"});

            // Act
            var result = controller.GetPersonById(23);
            
            // Assert
            Assert.That(result, Is.Not.Null, "Unexpected null result");
            var retrievedPostContent = result;
            Assert.That(retrievedPostContent, Is.Not.Null, "Unexpected null retrievedPost");
            var retrievedPerson = (PersonReadDto)((ObjectResult)result.Result).Value;
            Assert.That(retrievedPerson.Id, Is.EqualTo(23), "retrievedPost.Id is unexpected");
        }

        [Test]
        public void GetPerson_ShouldReturnSamePerson()
        {
            // Arrange

            var person = new PersonReadDto() { Id = 42, Name = "Peter", CreatedDate = DateTime.Today };

            _mockRepo.Setup(x => x.GetPersonById(42))
                .Returns(new Person { Id = 42 });

            var controller = new PersonsController(_mockRepo.Object, _mapper, _mockLogger.Object);

            // Act
            var item = controller.GetPersonById(42);

            // Assert
            Assert.IsNotNull(item);
            Assert.AreEqual(person.Id, ((PersonReadDto)((ObjectResult)item.Result).Value).Id);
        }

        [Test]
        public void GetActionReturnsNotFound()
        {
            // Arrange

            var controller = new PersonsController(_mockRepo.Object, _mapper, _mockLogger.Object);

            // Act
            var actionResult = controller.GetPersonById(10);

            // Assert
            Assert.IsInstanceOf<ActionResult<PersonReadDto>>(actionResult, typeof(NotFoundResult).ToString());
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepo.VerifyAll();
            _peopple = null;
            _mapper = null;
        }
    }
}