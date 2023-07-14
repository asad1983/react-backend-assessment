
using React_Backend.Application.Models;

namespace React_Backend.Application.Interfaces
{
    public interface IPatientService
    {
        public IEnumerable<object> GetAll();
        public string CreateAppointment(AppointmentModel model);
        public string DeleteAppointment(string appointmentId);
    }
}
