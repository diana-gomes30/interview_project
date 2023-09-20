using InterviewCalendarApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewCalendarApi.Data
{
    public class CalendarDbContext : DbContext
    {
        public CalendarDbContext(DbContextOptions<CalendarDbContext> options)
            : base(options)
        {
        }

        public DbSet<Interviewer> Interviewers { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<InterviewerSlot> InterviewerSlots { get; set; }
        public DbSet<CandidateSlot> CandidateSlots { get; set; }
    }
}
