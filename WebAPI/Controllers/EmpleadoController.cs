using AutoMapper;
using BusinessLogic.Data;
using BusinessLogic.Extension;
using Core.Interfaces;
using Core.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTO.PersonaFisica;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : BaseAPIController
    {

        private readonly IEmpleadoRepository _personaFisicaRepository;
        private readonly IMapper _mapper;
        public EmpleadoController(IEmpleadoRepository personaFisicaRepository, IMapper mapper)
        {
            _personaFisicaRepository = personaFisicaRepository;
            _mapper = mapper;
        }





        // GET: api/<EmpleadoController>
        [HttpPost("get")]
        public async Task<ActionResult<ServiceResponseList<IReadOnlyList<Empleado>>>> GetPersonasFisicas([FromBody] DtParameters dtParameters)
        {
            var data = await _personaFisicaRepository.GetAllWithSpec(dtParameters);
            return Ok(data);
               
        }

        // GET: api/<EmpleadoController>
        [HttpGet]
        public async Task<ActionResult<ServiceResponseList<IReadOnlyList<Empleado>>>> GetPersonasFisicas4()
        {
            var data = await _personaFisicaRepository.GetAll();
            return Ok(data);

        }

        // GET api/<EmpleadoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Empleado>>> GetPersonaFisica(int id)
        {
            var result = await _personaFisicaRepository.GetByIdWithSpec(id);
            if (result.Data == null) return NotFound();
            return Ok( result);
        }

        // POST api/<EmpleadoController>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Empleado>>> AddPersonaFisica(EmpleadoCreateDTO personaFisica)
        {
            var personaFisicaModel = _mapper.Map<EmpleadoCreateDTO, Empleado>(personaFisica);

            var validator = new EmpleadoValidator();
            ValidationResult resultsValidation = validator.Validate(personaFisicaModel);

            if (!resultsValidation.IsValid)
            {
                var response = new ServiceResponse<Empleado>();
                string message = "";
                foreach (var error in resultsValidation.Errors)
                {
                    message = message + error.ErrorMessage;
                }
                response.Success = false;
                response.Message = message;
                return BadRequest(response);
            }

            var result = await _personaFisicaRepository.Add(personaFisicaModel);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // PUT api/<EmpleadoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> Put(int id, [FromBody]  EmpleadoUpdateDTO personaFisica)
        {
            var validator = new EmpleadoValidatorUpdate();
            ValidationResult resultsValidation = validator.Validate(_mapper.Map<EmpleadoUpdateDTO, Empleado>(personaFisica));
            if (!resultsValidation.IsValid)
            {
                var response = new ServiceResponse<Empleado>();
                string message = "";
                foreach (var error in resultsValidation.Errors)
                {
                    message = message + error.ErrorMessage;
                }
                response.Success = false;
                response.Message = message;
                return BadRequest(response);
            }


            var result = await _personaFisicaRepository.Update(id, _mapper.Map<Empleado>(personaFisica));
            if (result.Data == null) return NotFound();
            if (result.Success) return Ok( result);
            return BadRequest(result);
        }

        // DELETE api/<EmpleadoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<int>>> Delete(int id)
        {
            var result = await _personaFisicaRepository.Delete(id);
            if (result.Data == null) return NotFound();
            if(result.Success) return Ok( result);
            return BadRequest(result);
        }
    }
}
