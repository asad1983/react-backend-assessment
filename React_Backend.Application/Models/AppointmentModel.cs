﻿using System.ComponentModel.DataAnnotations;

namespace React_Backend.Application.Models
{
    public  class AppointmentModel
    {
        
        public int Id { get; set; }
        public Guid AppointmentId { get; set; }
        public string Title { get; set; }
        public string? Detail { get; set; }

        public string DoctorId { get; set; }

        public string PatientId { get; set; }

        public DateTime AppointmentDateTime { get; set; }
        public string? Notes { get; set; }
    }
}
