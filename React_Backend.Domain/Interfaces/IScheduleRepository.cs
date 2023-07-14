using React_Backend.Domain.Entities;


namespace React_Backend.Domain.Interfaces
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        IEnumerable<Schedule> GetAll(string doctorId, string Day);
    }
}
