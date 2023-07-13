using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;

namespace React_Backend.Application.Services
{
    public class SpecialiasationService : ISpecialiasationService
    {
        public IEnumerable<SpecialiasationModel> GetAll()
        {
            var list= new List<SpecialiasationModel>();
            list.Add(new SpecialiasationModel
            {
                Id = 1,
                Name= "Allergists/Immunologists",
                Description = "They treat immune system disorders such as asthma, eczema, food allergies, insect sting allergies, and some autoimmune diseases."
            });
            list.Add(new SpecialiasationModel
            {
                Id = 2,
                Name = "Anesthesiologists",
                Description = "These doctors give you drugs to numb your pain or to put you under during surgery, childbirth, or other procedures. They monitor your vital signs while you’re under anesthesia."
            });
            list.Add(new SpecialiasationModel
            {
                Id = 3,
                Name = "Allergists/Immunologists",
                Description = "They’re experts on the heart and blood vessels. You might see them for heart failure, a heart attack, high blood pressure, or an irregular heartbeat."
            });
            return list;
        }
    }
}
