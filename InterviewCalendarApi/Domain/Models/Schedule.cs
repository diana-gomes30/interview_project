namespace InterviewCalendarApi.Domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public List<Day> Days { get; set; }
    }

    public class Day
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<Hour> Hours { get; set; }
    }

    public class Hour
    {
        public int Id { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
