using GymProgress.Domain.Models;

namespace GymProgress.Mobile.Interfaces
{
    public interface ISeancesService
    {
        Task<Seance> GetSeanceByName(string name);
        public Task<List<Seance>> GetAllSeance();
    }
}
