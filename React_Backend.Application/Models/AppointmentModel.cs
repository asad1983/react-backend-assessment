namespace React_Backend.Application.Models
{
    public  class AppointmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set;}

        public string DoctorName { get; set; }= string.Empty;
    }
}
