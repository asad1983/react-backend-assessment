using React_Backend.Domain.Entities;
using React_Backend.Domain.Interfaces;
using React_Backend.Domain.ViewModels;
using React_Backend.Infrastructure.Context;


namespace React_Backend.Infrastructure.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Appointment> GetAll()
        {
            var appointments = _context.Appointments;
            if (appointments != null) return appointments;
            return appointments!;
        }
        public IEnumerable<AppointmentViewModel> GetAll(AppointmentFilter model)
        {
            var result = (from appointment in _context.Appointments.Where(x => x.DoctorId == model.DoctorId && x.AppointmentDate== model.Date)
                          join doctor in _context.Users on appointment.PatientId equals doctor.Id
                          select new AppointmentViewModel {Title= appointment.Title, Detail = appointment.Detail, Date = appointment.AppointmentDate, Patient = $"{doctor.FirstName} {doctor.LastName}" , StartTime = appointment.StartTime,EndTime = appointment.EndTime, Id = appointment.AppointmentId });
            if (result != null) return result;
            return new List<AppointmentViewModel>();
        }
        public IEnumerable<Appointment> GetAll(string doctorId, DateOnly appointmentDate)
        {
            var result = _context.Appointments.Where(x=>x.DoctorId==doctorId && x.AppointmentDate== appointmentDate);
            if (result != null) return result;
            return new List<Appointment>();
        }
        public IEnumerable<object> GetAllPatientAppoints(string patientId)
        {
            var result = (from appointment in _context.Appointments.Where(x=>x.PatientId == patientId)
                          join patient in _context.Users on appointment.DoctorId equals patient.Id
                          select new { appointment.Title, appointment.Detail,Date= appointment.AppointmentDate, Doctor = $"{patient.FirstName} {patient.LastName}", appointment.StartTime, appointment.EndTime,Id=appointment.AppointmentId });
            return result!;
        }

        public Appointment Get(string appointmentId)
        {
            return _context.Appointments.FirstOrDefault(x=>x.AppointmentId.ToString()==appointmentId);
        }


    }
}
