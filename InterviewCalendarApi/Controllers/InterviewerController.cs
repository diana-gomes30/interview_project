using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Domain.Services;
using InterviewCalendarApi.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace InterviewCalendarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewerController : ControllerBase
    {
        private readonly IInterviewerService _interviewerService;
        public InterviewerController(IInterviewerService interviewerService)
        {
            _interviewerService = interviewerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse result = await _interviewerService.GetAll();

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ApiResponse result = await _interviewerService.Get(id);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Interviewer interviewer)
        {
            ApiResponse result = await _interviewerService.Post(interviewer);
            if (result.StatusCode == 201)
                return Created("", result);

            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _interviewerService.Delete(id);
            return NoContent();
        }

        [HttpGet("getSlots/{id}")]
        public async Task<IActionResult> GetSlots(int id)
        {
            ApiResponse result = await _interviewerService.GetSlots(id);

            if (result.StatusCode == 201)
                return Created("", result);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> PostSchedule([FromBody] Schedule schedule)
        {
            ApiResponse result = await _interviewerService.PostSchedule(schedule);

            if (result.StatusCode == 201)
                return Created("", result);

            return StatusCode(result.StatusCode, result);
        }
    }
}
