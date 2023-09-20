namespace InterviewCalendarApi.Domain.Models
{
    public class CandidateSlot
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime Date { get; set; }
        public string WeekDay { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
