using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Domain.Services;
using InterviewCalendarApi.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace InterviewCalendarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse result = await _candidateService.GetAll();

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ApiResponse result = await _candidateService.Get(id);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Candidate candidate)
        {
            ApiResponse result = await _candidateService.Post(candidate);
            if (result.StatusCode == 201)
                return Created("", result);

            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _candidateService.Delete(id);
            return NoContent();
        }

        [HttpGet("getSlots/{id}")]
        public async Task<IActionResult> GetSlots(int id)
        {
            ApiResponse result = await _candidateService.GetSlots(id);

            if (result.StatusCode == 201)
                return Created("", result);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> PostSchedule([FromBody] Schedule schedule)
        {
            ApiResponse result = await _candidateService.PostSchedule(schedule);

            if (result.StatusCode == 201)
                return Created("", result);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("getAvailableSlots/{id}")]
        public async Task<IActionResult> GetAvailableSlots(int id)
        {
            ApiResponse result = await _candidateService.GetAvailableSlots(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
