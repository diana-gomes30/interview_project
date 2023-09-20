using InterviewCalendarApi.Data;
using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewCalendarApi.Repositories
{
    public class CandidateRepository : IBaseRepository<Candidate>
    {
        private readonly CalendarDbContext _dbContext;
        public CandidateRepository(CalendarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Candidate>> GetAll()
        {
            List<Candidate> candidates = await _dbContext.Candidates.ToListAsync<Candidate>();
            return candidates;
        }

        public async Task<Candidate> Get(int id)
        {
            Candidate? candidate = await _dbContext.Candidates.Where(i => i.Id == id).FirstOrDefaultAsync();
            return candidate!;
        }

        public async Task<Candidate> Post(Candidate candidate)
        {
            _dbContext.Candidates.Add(candidate);
            await _dbContext.SaveChangesAsync();

            Candidate newCandidate = await _dbContext.Candidates.Where(i => i.Id == candidate.Id).FirstAsync();
            return newCandidate;
        }

        public Task<Candidate> Put(int id, Candidate candidate)
        {
            throw new NotImplementedException();
        }

        public async void Delete(int id)
        {
            Candidate? candidate = await _dbContext.Candidates.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (candidate != null)
            {
                _dbContext.Candidates.Remove(candidate);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
