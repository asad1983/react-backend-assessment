
using System.Text.Json.Serialization;

namespace React_Backend.Application.Models
{
    public class AppointmentFilter
    {
        public DateOnly? Date { get; set; }


        [JsonIgnore]
        public string DoctorId { get; set; }


    }
}
