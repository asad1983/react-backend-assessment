
using React_Backend.Application.Models;

namespace React_Backend.Application.Interfaces
{
    public interface IPatientService
    {
        public IEnumerable<object> GetAll(string patientId);
        public string CreateAppointment(AppointmentModel model);
        public string DeleteAppointment(string appointmentId);
    }
}
