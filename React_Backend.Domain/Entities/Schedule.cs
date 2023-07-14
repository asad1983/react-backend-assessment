using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace React_Backend.Domain.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("DoctorId")]
        public ApplicationUser Doctor { get; set; }
        public string DoctorId { get; set;}
        public string Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool Status { get; set; }
    }
}
