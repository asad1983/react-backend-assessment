using React_Backend.Domain.Entities;


namespace React_Backend.Domain.Interfaces
{
    public interface IAppointmentRepository: IGenericRepository<Appointment>
    {
        
        IEnumerable<Appointment> GetAll();
        IEnumerable<Appointment> GetAllPatientAppoints(string patientId);


    }
}
