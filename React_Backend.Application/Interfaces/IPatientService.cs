
using React_Backend.Application.Models;

namespace React_Backend.Application.Interfaces
{
    public interface IPatientService
    {
        public IEnumerable<object> GetAll();
        public void CreateAppointment(AppointmentModel model);
        public void DeleteAppointment(string appointmentId);
    }
}
