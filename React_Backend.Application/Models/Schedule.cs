

using System.ComponentModel.DataAnnotations;

namespace React_Backend.Application.Models
{
    public class Schedule
    {
        
      
       // public string DoctorId { get; set;}
        public string Day { get; set; }

        [DataType(DataType.Time)]
        public TimeOnly StartTime { get; set; }

        [DataType(DataType.Time)]
        public TimeOnly EndTime { get; set; }
       // public bool Status { get; set; }
    }
}
