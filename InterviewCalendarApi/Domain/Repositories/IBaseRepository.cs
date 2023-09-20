namespace InterviewCalendarApi.Domain.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Post(T t);
        Task<T> Put(int id, T t);
        void Delete(int id);
    }
}
