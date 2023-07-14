using React_Backend.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        
        public DateOnly AppointmentDate{ get; set;}
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public EnumEntities.AppointmentStatus Status { get; set; }
        public string? Notes { get; set;}
    }
}
