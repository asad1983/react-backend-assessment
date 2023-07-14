using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace React_Backend.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public Guid AppointmentId { get; set; }
        public string Title { get; set; }
        public string? Detail { get; set; }

        public string DoctorId { get; set; }
        
        public string PatientId { get; set; }
        
        public DateTime AppointmentDateTime{ get; set;}
        public string? Notes { get; set;}
    }
}
