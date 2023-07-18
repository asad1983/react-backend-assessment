


namespace React_Backend.Application.Models.ViewModels
{
    public class AppointmentViewModel
    {
        
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Detail { get; set; }

        public string Patient { get; set; }
       
        public DateOnly Date { get; set;}
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

    }
}
