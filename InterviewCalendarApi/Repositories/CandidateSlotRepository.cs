using InterviewCalendarApi.Data;
using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewCalendarApi.Repositories
{
    public class CandidateSlotRepository : ISlotRepository<CandidateSlot>
    {
        private readonly CalendarDbContext _dbContext;
        public CandidateSlotRepository(CalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CandidateSlot>> GetAll()
        {
            List<CandidateSlot> slots = await _dbContext.CandidateSlots.ToListAsync<CandidateSlot>();
            return slots;
        }

        public async Task<CandidateSlot> Get(int id)
        {
            CandidateSlot? slot = await _dbContext.CandidateSlots.Where(i => i.Id == id).FirstOrDefaultAsync();
            return slot!;
        }

        public async Task<CandidateSlot> Post(CandidateSlot slot)
        {
            _dbContext.CandidateSlots.Add(slot);
            await _dbContext.SaveChangesAsync();

            CandidateSlot newSlot = await _dbContext.CandidateSlots.Where(i => i.Id == slot.Id).FirstAsync();
            return newSlot;
        }

        public Task<CandidateSlot> Put(int id, CandidateSlot t)
        {
            throw new NotImplementedException();
        }

        public async void Delete(int id)
        {
            CandidateSlot? slot = await _dbContext.CandidateSlots.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (slot != null)
            {
                _dbContext.CandidateSlots.Remove(slot);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<CandidateSlot> GetByPersonDateTime(int personId, DateTime date, int startTime, int endTime)
        {
            CandidateSlot? slot = await _dbContext.CandidateSlots
                .Where(i => i.Id == personId && i.Date.Date == date.Date && i.StartTime == startTime && i.EndTime == endTime)
                .FirstOrDefaultAsync();
            return slot!;
        }

        public async Task<CandidateSlot> GetByDateTime(DateTime date, int startTime, int endTime)
        {
            CandidateSlot? slot = await _dbContext.CandidateSlots
                .Where(i => i.Date.Date == date.Date && i.StartTime == startTime && i.EndTime == endTime)
                .FirstOrDefaultAsync();
            return slot!;
        }

        public async Task<List<CandidateSlot>> GetByPerson(int id)
        {
            List<CandidateSlot> slots = await _dbContext.CandidateSlots
                .Where(i => i.PersonId == id)
                .ToListAsync();

            return slots;
        }
    }
}
