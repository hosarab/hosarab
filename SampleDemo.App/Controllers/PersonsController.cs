using System.Collections.Generic;
using AutoMapper;
using SampleDemo.Data;
using SampleDemo.Dtos;
using SampleDemo.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace SampleDemo.Controllers
{

    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    { 
        private readonly ISampleDemoRepo _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonsController> _logger;
        public PersonsController(ISampleDemoRepo repository, IMapper mapper, ILogger<PersonsController> logger)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
       
        //GET api/persons
        [HttpGet]
        public ActionResult <IEnumerable<PersonReadDto>> GetAllPersons()
        {
            var personItems = _repository.GetAllPersons();

            return Ok(_mapper.Map<IEnumerable<PersonReadDto>>(personItems));
        }

        //GET api/persons/{id}
        [HttpGet("{id}", Name="GetPersonById")]
        public ActionResult <PersonReadDto> GetPersonById(int id)
        {
            var personItem = _repository.GetPersonById(id);
            if(personItem != null)
            {
                return Ok(_mapper.Map<PersonReadDto>(personItem));
            }
            return NotFound();
        }

        //POST api/persons
        [HttpPost]
        public ActionResult <PersonReadDto> CreatePerson(PersonCreateDto personCreateDto)
        {
            if (personCreateDto == null)
            {
                _logger.LogError("Owner object sent from client is null.");
                return BadRequest("Owner object is null");
            }
            
            var parsonModel = _mapper.Map<Person>(personCreateDto);
            _repository.CreatePerson(parsonModel);
            _repository.SaveChanges();

            var personReadDto = _mapper.Map<PersonReadDto>(parsonModel);

            return CreatedAtRoute(nameof(GetPersonById), new {personReadDto.Id}, personReadDto);      
        }

        //PUT api/persons/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatePerson(int id, PersonUpdateDto personUpdateDto)
        {
            if (personUpdateDto == null)
            {
                _logger.LogError("Owner object sent from client is null.");
                return BadRequest("Owner object is null");
            }

            var personModelFromRepo = _repository.GetPersonById(id);
            if(personModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(personUpdateDto, personModelFromRepo);

            _repository.UpdatePerson(personModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/persons/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialPersonUpdate(int id, JsonPatchDocument<PersonUpdateDto> patchDoc)
        {
            var personModelFromRepo = _repository.GetPersonById(id);
            if(personModelFromRepo == null)
            {
                return NotFound();
            }

            var personToPatch = _mapper.Map<PersonUpdateDto>(personModelFromRepo);
            patchDoc.ApplyTo(personToPatch, ModelState);

            if(!TryValidateModel(personToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(personToPatch, personModelFromRepo);

            _repository.UpdatePerson(personModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/persons/{id}
        [HttpDelete("{id}")]
        public ActionResult DeletePerson(int id)
        {
            var personModelFromRepo = _repository.GetPersonById(id);
            if(personModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeletePerson(personModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        
    }
}