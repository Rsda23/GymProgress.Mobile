using GymProgress.Domain.Models;

namespace GymProgress.Mobile.Interfaces
{
    public interface ISeancesService
    {
        Task<Seance> GetSeanceByName(string name);
        public Task<List<Seance>> GetAllSeance();
        public Task<bool> PostSeance(Seance seance);
        public Task<bool> AddExerciceToSeanceById(string seanceId, List<string> execicesId);
        public Task<bool> Delete(string seanceId);
    }
}
