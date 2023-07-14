using React_Backend.Domain.Entities;
using React_Backend.Domain.Interfaces;
using React_Backend.Infrastructure.Context;

namespace React_Backend.Infrastructure.Repositories
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        private readonly ApplicationDbContext _context;
        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Schedule> GetAll(string doctorId, string day)
        {
            var schedules = _context.Schedules.Where(x=>x.DoctorId==doctorId && x.Day== day);
            if (schedules != null) return schedules;
            return new List<Schedule>();
        }


    }
}
