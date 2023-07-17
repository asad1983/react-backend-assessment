using React_Backend.Domain.Entities;


namespace React_Backend.Domain.Interfaces
{
    public interface IAppointmentRepository: IGenericRepository<Appointment>
    {
        
        IEnumerable<Appointment> GetAll();
        IEnumerable<object> GetAll(AppointmentFilter model);
        IEnumerable<Appointment> GetAll(string doctorId,DateOnly appointmentDate);
        IEnumerable<object> GetAllPatientAppoints(string patientId);
        Appointment Get(string appointmentId);

    }
}
