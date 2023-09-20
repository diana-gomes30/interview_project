using InterviewCalendarApi.Data;
using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewCalendarApi.Repositories
{
    public class InterviewerSlotRepository : ISlotRepository<InterviewerSlot>
    {
        private readonly CalendarDbContext _dbContext;
        public InterviewerSlotRepository(CalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InterviewerSlot>> GetAll()
        {
            List<InterviewerSlot> slots = await _dbContext.InterviewerSlots.ToListAsync<InterviewerSlot>();
            return slots;
        }

        public async Task<InterviewerSlot> Get(int id)
        {
            InterviewerSlot? slot = await _dbContext.InterviewerSlots.Where(i => i.Id == id).FirstOrDefaultAsync();
            return slot!;
        }

        public async Task<InterviewerSlot> Post(InterviewerSlot slot)
        {
            _dbContext.InterviewerSlots.Add(slot);
            await _dbContext.SaveChangesAsync();

            InterviewerSlot newSlot = await _dbContext.InterviewerSlots.Where(i => i.Id == slot.Id).FirstAsync();
            return newSlot;
        }

        public Task<InterviewerSlot> Put(int id, InterviewerSlot t)
        {
            throw new NotImplementedException();
        }

        public async void Delete(int id)
        {
            InterviewerSlot? slot = await _dbContext.InterviewerSlots.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (slot != null)
            {
                _dbContext.InterviewerSlots.Remove(slot);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<InterviewerSlot> GetByPersonDateTime(int personId, DateTime date, int startTime, int endTime)
        {
            InterviewerSlot? slot = await _dbContext.InterviewerSlots
                .Where(i => i.Id == personId && i.Date.Date == date.Date && i.StartTime == startTime && i.EndTime == endTime)
                .FirstOrDefaultAsync();
            return slot!;
        }

        public async Task<InterviewerSlot> GetByDateTime(DateTime date, int startTime, int endTime)
        {
            InterviewerSlot? slot = await _dbContext.InterviewerSlots
                .Where(i => i.Date.Date == date.Date && i.StartTime == startTime && i.EndTime == endTime)
                .FirstOrDefaultAsync();
            return slot!;
        }

        public async Task<List<InterviewerSlot>> GetByPerson(int id)
        {
            List<InterviewerSlot> slots = await _dbContext.InterviewerSlots
                .Where(i => i.PersonId == id)
                .ToListAsync();

            return slots;
        }
    }
}
