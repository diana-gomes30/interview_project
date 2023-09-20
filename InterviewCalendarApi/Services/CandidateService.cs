using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Domain.Repositories;
using InterviewCalendarApi.Domain.Services;
using InterviewCalendarApi.Utilities;

namespace InterviewCalendarApi.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IBaseRepository<Candidate> _candidateRepository;
        private readonly ISlotRepository<CandidateSlot> _candidateSlotRepository;
        private readonly ISlotRepository<InterviewerSlot> _interviewerSlotRepository;

        public CandidateService(IBaseRepository<Candidate> candidateRepository, ISlotRepository<CandidateSlot> slotRepository, ISlotRepository<InterviewerSlot> interviewerSlotRepository)
        {
            _candidateRepository = candidateRepository;
            _candidateSlotRepository = slotRepository;
            _interviewerSlotRepository = interviewerSlotRepository;
        }

        public async Task<ApiResponse> GetAll()
        {
            try
            {
                List<Candidate> candidates = await _candidateRepository.GetAll();
                return new ApiResponse(200, result: candidates);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ApiResponse> Get(int id)
        {
            try
            {
                Candidate? candidate = await _candidateRepository.Get(id);
                if (candidate != null)
                    return new ApiResponse(200, result: candidate);

                return new ApiResponse(404);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ApiResponse> Post(Candidate candidate)
        {
            try
            {
                candidate.Id = 0;
                Candidate newCandidate = await _candidateRepository.Post(candidate);
                return new ApiResponse(201, result: newCandidate);
            }
            catch (Exception e)
            {
                return new ApiResponse(400, e.Message);
            }
        }

        public Task<ApiResponse> Put(int id, Candidate t)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> Delete(int id)
        {
            try
            {
                _candidateRepository.Delete(id);
                return Task.FromResult(new ApiResponse(204));
            }
            catch (Exception e)
            {
                return Task.FromResult(new ApiResponse(400, e.Message));
            }
        }

        public async Task<ApiResponse> GetSlots(int id)
        {
            try
            {
                List<CandidateSlot> slots = await _candidateSlotRepository.GetByPerson(id);
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
                Candidate? candidate = await _candidateRepository.Get(schedule.PersonId);
                if (candidate != null)
                {
                    List<CandidateSlot> createdSlots = new();
                    foreach (Day day in schedule.Days)
                    {
                        foreach (Hour hour in day.Hours)
                        {
                            if (day.Date.Date <= DateTime.Now.Date)
                                return new ApiResponse(400, result: "Date slot is invalid: Date must be greater than current date.");

                            if (hour.StartTime >= hour.EndTime)
                                return new ApiResponse(400, result: "Time slot is invalid. Start time can't be greater than end time.");

                            if (hour.StartTime < 0 || hour.StartTime >= 24 || hour.EndTime < 0 || hour.EndTime >= 24)
                                return new ApiResponse(400, result: "Time Slot is invalid. Must be greater than 0 and less than 24.");

                            int numberSlots = hour.EndTime - hour.StartTime;
                            for (int i = 0; i < numberSlots; i++)
                            {
                                int startTime = hour.StartTime + i;
                                int endTime = startTime + 1;

                                CandidateSlot? slotExists = await _candidateSlotRepository.GetByPersonDateTime(candidate.Id, day.Date, startTime, endTime);
                                if (slotExists == null)
                                {
                                    CandidateSlot slot = new()
                                    {
                                        Id = 0,
                                        PersonId = candidate!.Id,
                                        Date = day.Date.Date,
                                        WeekDay = day.Date.Date.DayOfWeek.ToString(),
                                        StartTime = startTime,
                                        EndTime = endTime,
                                    };

                                    CandidateSlot newSlot = await _candidateSlotRepository.Post(slot);
                                    createdSlots.Add(newSlot);
                                }
                            }
                        }
                    }

                    return new ApiResponse(201, result: createdSlots);
                }
                else
                {
                    return new ApiResponse(404, result: "Candidate not found");
                }

            }
            catch (Exception e)
            {
                return new ApiResponse(400, e.Message);
            }
        }

        public async Task<ApiResponse> GetAvailableSlots(int id)
        {
            try
            {
                List<CandidateSlot> availableSlots = new();

                List<CandidateSlot> candidateSlots = await _candidateSlotRepository.GetByPerson(id);
                foreach (CandidateSlot candidateSlot in candidateSlots)
                {
                    InterviewerSlot interviewerSlot = await _interviewerSlotRepository.GetByDateTime(candidateSlot.Date, candidateSlot.StartTime, candidateSlot.EndTime);
                    if (interviewerSlot != null)
                    {
                        availableSlots.Add(candidateSlot);
                    }
                }

                return new ApiResponse(200, result: availableSlots);
            }
            catch (Exception e)
            {
                return new ApiResponse(400, e.Message);
            }
        }
    }
}
