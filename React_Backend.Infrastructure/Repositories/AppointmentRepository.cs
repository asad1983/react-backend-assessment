using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Appointment> GetAllPatientAppoints(string patientId)
        {
            var query = _context.Appointments.Where(x=>x.PatientId==patientId);
            if (query != null) return query;
            return null!;
        }

    }
}
