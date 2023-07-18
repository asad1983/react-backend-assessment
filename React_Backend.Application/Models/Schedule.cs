

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public string? DoctorId { get; set; }

        [JsonIgnore]
        public bool Status { get; set; } = true;
    }
}
