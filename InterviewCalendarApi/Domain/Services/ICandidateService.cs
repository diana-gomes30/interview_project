using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Utilities;

namespace InterviewCalendarApi.Domain.Services
{
    public interface ICandidateService : IBaseService<Candidate>
    {
        Task<ApiResponse> PostSchedule(Schedule schedule);
        Task<ApiResponse> GetSlots(int id);
        Task<ApiResponse> GetAvailableSlots(int id);
    }
}
