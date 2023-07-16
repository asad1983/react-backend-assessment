using React_Backend.Domain.Entities;
using React_Backend.Domain.Interfaces;
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
            var query = _context.Appointments;
            if (query != null) return query;
            return null!;
        }
        public IEnumerable<object> GetAll(string doctorId)
        {
            var dateToday= DateTime.Now;
            DateTime formatDateToday = new DateTime(dateToday.Year, dateToday.Month, dateToday.Day);
            DateOnly formatDateOnly = DateOnly.FromDateTime(formatDateToday);
            var result = (from appointment in _context.Appointments.Where(x => x.DoctorId == doctorId && x.AppointmentDate>= formatDateOnly)
                          join doctor in _context.Users on appointment.PatientId equals doctor.Id
                          select new { appointment.Title, appointment.Detail, Date = appointment.AppointmentDate, Patient = $"{doctor.FirstName} {doctor.LastName}" , appointment.StartTime,appointment.EndTime, Id = appointment.AppointmentId });
            if (result != null) return result;
            return new List<object>();
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
            //var query = _context.Appointments.Where(x=>x.PatientId==patientId);
            //if (query != null) return query;
            return result!;
        }

        public Appointment Get(string appointmentId)
        {
            return _context.Appointments.FirstOrDefault(x=>x.AppointmentId.ToString()==appointmentId);
        }


    }
}
