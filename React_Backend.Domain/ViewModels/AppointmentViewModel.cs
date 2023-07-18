using React_Backend.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace React_Backend.Domain.ViewModels
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
