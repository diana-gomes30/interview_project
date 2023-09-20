using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Domain.Services;

namespace InterviewCalendarApi.Domain.Repositories
{
    public interface ISlotRepository<T> : IBaseRepository<T>
    {
        Task<T> GetByPersonDateTime(int personId, DateTime date, int startTime, int endTime);
        Task<T> GetByDateTime(DateTime date, int startTime, int endTime);
        Task<List<T>> GetByPerson(int id);
    }
}
