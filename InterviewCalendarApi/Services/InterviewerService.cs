using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Domain.Repositories;
using InterviewCalendarApi.Domain.Services;
using InterviewCalendarApi.Utilities;

namespace InterviewCalendarApi.Services
{
    public class InterviewerService : IInterviewerService
    {
        private readonly IBaseRepository<Interviewer> _interviewerRepository;
        private readonly ISlotRepository<InterviewerSlot> _interviewerSlotRepository;
        public InterviewerService(IBaseRepository<Interviewer> interviewerRepository, ISlotRepository<InterviewerSlot> interviewerSlotRepository)
        {
            _interviewerRepository = interviewerRepository;
            _interviewerSlotRepository = interviewerSlotRepository;
        }

        public async Task<ApiResponse> GetAll()
        {
            try
            {
                List<Interviewer> interviewers = await _interviewerRepository.GetAll();
                return new ApiResponse(200, result: interviewers);
            }
            catch (Exception e)
            {
                return new ApiResponse(400, e.Message);
            }
        }

        public async Task<ApiResponse> Get(int id)
        {
            try
            {
                Interviewer? interviewer = await _interviewerRepository.Get(id);
                if (interviewer != null)
                    return new ApiResponse(200, result: interviewer);

                return new ApiResponse(404);
            }
            catch (Exception e)
            {
                return new ApiResponse(400, result: e.Message);
            }
        }

        public async Task<ApiResponse> Post(Interviewer interviewer)
        {
            try
            {
                interviewer.Id = 0;
                Interviewer newInterviewer = await _interviewerRepository.Post(interviewer);
                return new ApiResponse(201, result: newInterviewer);
            }
            catch (Exception e)
            {
                return new ApiResponse(400, result: e.Message);
            }
        }

        public Task<ApiResponse> Put(int id, Interviewer interviewer)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Delete(int id)
        {
            try
            {
                _interviewerRepository.Delete(id);
                return Task.FromResult(new ApiResponse(204));
            }
            catch (Exception e)
            {
                return Task.FromResult(new ApiResponse(400, result: e.Message));
            }
        }

        public async Task<ApiResponse> GetSlots(int id)
        {
            try
            {
                List<InterviewerSlot> slots = await _interviewerSlotRepository.GetByPerson(id);
                return new ApiResponse(200, result: slots);
            }
            catch (Exception e)
            {
                return new ApiResponse(400, e.Message);
            }
        }

        public async Task<ApiResponse> PostSchedule(Schedule schedule)
        {
            try
            {
                Interviewer? interviewer = await _interviewerRepository.Get(schedule.PersonId);
                if (interviewer != null)
                {
                    List<InterviewerSlot> createdSlots = new();
                    foreach (Day day in schedule.Days)
                    {
                        foreach (Hour hour in day.Hours)
                        {
                            if (day.Date.Date <= DateTime.Now.Date)
                                return new ApiResponse(400, result: "Date slot is invalid: Date must be greater than current date.");

                            if (hour.StartTime >= hour.EndTime)
                                return new ApiResponse(400, result: "Time slot is invalid. Start time can't be greater than end time.");

                            if (hour.StartTime < 0 || hour.StartTime >= 24 || hour.EndTime < 0 || hour.EndTime >= 24)
                                return new ApiResponse(400, result: "Time Slot is invalid. Must be greater than 0 and less than 24");

                            int numberSlots = hour.EndTime - hour.StartTime;
                            for (int i = 0; i < numberSlots; i++)
                            {
                                int startTime = hour.StartTime + i;
                                int endTime = startTime + 1;

                                InterviewerSlot? slotExists = await _interviewerSlotRepository.GetByPersonDateTime(interviewer.Id, day.Date, startTime, endTime);
                                if (slotExists == null)
                                {
                                    InterviewerSlot slot = new()
                                    {
                                        Id = 0,
                                        PersonId = interviewer!.Id,
                                        Date = day.Date.Date,
                                        WeekDay = day.Date.Date.DayOfWeek.ToString(),
                                        StartTime = startTime,
                                        EndTime = endTime,
                                    };

                                    InterviewerSlot newSlot = await _interviewerSlotRepository.Post(slot);
                                    createdSlots.Add(newSlot);
                                }
                            }

                        }
                    }

                    return new ApiResponse(201, result: createdSlots);
                }
                else
                {
                    return new ApiResponse(404, result: "Interviewer not found");
                }

            }
            catch (Exception e)
            {
                return new ApiResponse(400, e.Message);
            }
        }
    }
}
