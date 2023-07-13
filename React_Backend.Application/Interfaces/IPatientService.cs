
using React_Backend.Application.Models;

namespace React_Backend.Application.Interfaces
{
    public interface IPatientService
    {
        public IEnumerable<AppointmentModel> GetAll();
    }
}
