using System.ComponentModel.DataAnnotations;

namespace React_Backend.Application.Models
{
    public  class AppointmentModel
    {


        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        public string? Detail { get; set; }

        [Required(ErrorMessage = "Doctor Id is required.")]
        public string DoctorId { get; set; }

        [Required(ErrorMessage = "Appointment Date is required.")]
       
        public DateOnly AppointmentDate { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Start Time is required.")]
        public TimeOnly StartTime { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "End Time is required.")]
        public TimeOnly EndTime { get; set; }
        public string? Notes { get; set; }
    }
}
