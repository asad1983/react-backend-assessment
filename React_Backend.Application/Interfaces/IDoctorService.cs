
using React_Backend.Application.Models;

namespace React_Backend.Application.Interfaces
{
    public interface IDoctorService
    {
        public IEnumerable<AppointmentModel> GetAll();
    }
}
