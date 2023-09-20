using InterviewCalendarApi.Controllers;
using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Domain.Repositories;
using InterviewCalendarApi.Domain.Services;
using InterviewCalendarApi.Services;
using InterviewCalendarApi.Utilities;
using Moq;

namespace InterviewCalendarApiTests
{
    public class InterviewerTests
    {
        private IInterviewerService _service;
        private Mock<IBaseRepository<Interviewer>> _mockRepository;
        private Mock<ISlotRepository<InterviewerSlot>> _mockSlotRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IBaseRepository<Interviewer>>(MockBehavior.Strict);
            _mockSlotRepository = new Mock<ISlotRepository<InterviewerSlot>>(MockBehavior.Strict);
            _service = new InterviewerService(_mockRepository.Object, _mockSlotRepository.Object);

        }

        [Test]
        public async Task ShouldCreateInterviewer()
        {
            Interviewer interviewer = new()
            {
                Id = 1,
                Name = "Ana"
            };

            _mockRepository.Setup(x => x.Post(interviewer)).ReturnsAsync(interviewer);

            ApiResponse apiResponse = new(201, result: interviewer);

            var createdResponse = await _service.Post(interviewer);

            Assert.NotNull(createdResponse);
            Assert.That(createdResponse.StatusCode, Is.EqualTo(apiResponse.StatusCode));
            Assert.That(createdResponse.Data, Is.EqualTo(apiResponse.Data));
        }

        [Test]
        public async Task ShouldGetInterviewer()
        {
            Interviewer interviewer = new()
            {
                Id = 1,
                Name = "Ana"
            };

            _mockRepository.Setup(x => x.Get(interviewer.Id)).ReturnsAsync(interviewer);

            var getResponse = await _service.Get(1);

            Assert.NotNull(getResponse);
            Assert.That(getResponse.Data, Is.EqualTo(interviewer));
        }

        [Test]
        public async Task ShouldGetAllInterviewers()
        {
            List<Interviewer> interviewers = new();
            Interviewer interviewer1 = new() {
                Id = 1,
                Name = "Ana"
            };
            Interviewer interviewer2 = new()
            {
                Id = 1,
                Name = "Pedro"
            };

            interviewers.Add(interviewer1);
            interviewers.Add(interviewer2);

            _mockRepository.Setup(x => x.GetAll()).ReturnsAsync(interviewers);

            var getAllResponse = await _service.GetAll();

            Assert.NotNull(getAllResponse);
            Assert.That(getAllResponse.StatusCode, Is.EqualTo(200));
            Assert.That(getAllResponse.Data, Is.EqualTo(interviewers));
        }
    }
}