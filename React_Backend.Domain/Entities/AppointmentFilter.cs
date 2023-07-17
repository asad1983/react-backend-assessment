
using System.Text.Json.Serialization;

namespace React_Backend.Domain.Entities
{
    public class AppointmentFilter
    {
        public DateOnly? Date { get; set; }
        public string DoctorId { get; set; }
    }
}
