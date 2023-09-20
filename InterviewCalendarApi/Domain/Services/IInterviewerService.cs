using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Utilities;

namespace InterviewCalendarApi.Domain.Services
{
    public interface IInterviewerService : IBaseService<Interviewer>
    {
        Task<ApiResponse> PostSchedule(Schedule schedule);
        Task<ApiResponse> GetSlots(int id);
    }
}
