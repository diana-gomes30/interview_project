using InterviewCalendarApi.Domain.Models;
using InterviewCalendarApi.Utilities;

namespace InterviewCalendarApi.Domain.Services
{
    public interface IBaseService<T>
    {
        Task<ApiResponse> GetAll();
        Task<ApiResponse> Get(int id);
        Task<ApiResponse> Post(T t);
        Task<ApiResponse> Put(int id, T t);
        Task<ApiResponse> Delete(int id);
    }
}
