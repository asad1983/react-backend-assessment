
using React_Backend.Application.Models;
using React_Backend.Application.Models.ViewModels;

namespace React_Backend.Application.Interfaces
{
    public interface IDoctorService
    {
        public IEnumerable<AppointmentViewModel> GetAll(AppointmentFilter model);
    }
}
