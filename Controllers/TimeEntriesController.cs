using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimesheetApi.Dtos;
using TimesheetApi.Services;

namespace TimesheetApi.Controllers
{
    [ApiController]
    [Route("timeentries")]
    public class TimeEntriesController : ControllerBase
    {
        private readonly ITimeEntryService _service;

        public TimeEntriesController(ITimeEntryService service)
        {
            _service = service;
        }

        // GET /timeentries            -> alle registraties
        // GET /timeentries?projectId=1 -> enkel die van project 1
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? projectId)
        {
            var entries = await _service.GetAllAsync(projectId);
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _service.GetByIdAsync(id);
            if (entry is null)
            {
                return NotFound();
            }

            return Ok(entry);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(TimeEntryInputDto dto)
        {
            // de gebruikersnaam halen we uit de claims van het JWT-token
            var username = User.FindFirstValue(ClaimTypes.Name) ?? "";

            var (result, created) = await _service.CreateAsync(dto, username);

            if (result == CreateTimeEntryResult.ProjectNotFound)
            {
                return Problem(
                    title: "Dit project bestaat niet",
                    statusCode: StatusCodes.Status400BadRequest);
            }

            if (result == CreateTimeEntryResult.ProjectClosed)
            {
                return Problem(
                    title: "Dit project is afgesloten, je kan er geen uren meer op boeken",
                    statusCode: StatusCodes.Status409Conflict);
            }

            return CreatedAtAction(nameof(GetById), new { id = created!.Id }, created);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
