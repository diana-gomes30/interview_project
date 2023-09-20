using InterviewCalendarApi.Data;
using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewCalendarApi.Repositories
{
    public class InterviewerRepository : IBaseRepository<Interviewer>
    {
        private readonly CalendarDbContext _dbContext;
        public InterviewerRepository(CalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Interviewer>> GetAll()
        {
            List<Interviewer> interviewers = await _dbContext.Interviewers.ToListAsync<Interviewer>();
            return interviewers;
        }

        public async Task<Interviewer> Get(int id)
        {
            Interviewer? interviewer = await _dbContext.Interviewers.Where(i => i.Id == id).FirstOrDefaultAsync();
            return interviewer!;
        }

        public async Task<Interviewer> Post(Interviewer interviewer)
        {
            _dbContext.Interviewers.Add(interviewer);
            await _dbContext.SaveChangesAsync();

            Interviewer newInterviewer = await _dbContext.Interviewers.Where(i => i.Id == interviewer.Id).FirstAsync();
            return newInterviewer;
        }

        public Task<Interviewer> Put(int id, Interviewer interviewer)
        {
            throw new NotImplementedException();
        }

        public async void Delete(int id)
        {
            Interviewer? interviewer = await _dbContext.Interviewers.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (interviewer != null)
            {
                _dbContext.Interviewers.Remove(interviewer);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
