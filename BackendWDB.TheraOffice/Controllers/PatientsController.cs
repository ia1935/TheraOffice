using Microsoft.AspNetCore.Mvc;
using Backend.TheraOffice.Models;
using Backend.TheraOffice.Repositories;
using Backend.TheraOffice.DTOs;

namespace Backend.TheraOffice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repo;

        public PatientsController(IPatientRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetAll()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Patient> GetById(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public ActionResult<Patient> Create([FromBody] PatientCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth
            };
            var created = _repo.Create(patient);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public ActionResult Update(int id, [FromBody] PatientUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existing = _repo.GetById(id);
            if (existing == null) return NotFound();
            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Email = dto.Email;
            existing.DateOfBirth = dto.DateOfBirth;
            _repo.Update(existing);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var success = _repo.Delete(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Patient>> Search([FromQuery] string q)
        {
            return Ok(_repo.Search(q));
        }
    }
}
