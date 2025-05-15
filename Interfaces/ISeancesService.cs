using GymProgress.Domain.Models;

namespace GymProgress.Mobile.Interfaces
{
    public interface ISeancesService
    {
        Task<Seance?> GetSeanceByName(string name);
        public Task<Seance?> GetSeanceById(string id);
        public Task<List<Seance>?> GetAllSeance();
        public Task<List<Seance>?> GetSeanceByUserId(string userId);
        public Task<List<Seance>?> GetSeancePublic();
        public Task<List<Seance>?> GetLastSeance(int count, string userId);
        public Task<bool> PostSeance(Seance seance);
        public Task<bool> AddExerciceToSeanceById(string seanceId, List<string> execicesId);
        public Task<bool> Delete(string seanceId);
        public Task<bool> RemoveExerciceToSeance(string seanceId, string exerciceId);
    }
}
