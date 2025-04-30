using GymProgress.Domain.Models;

namespace GymProgress.Mobile.Interfaces
{
    public interface IExercicesService
    {
        public Task<List<Exercice>> GetAllExercice();
        public Task<Exercice> GetExerciceByName(string name);
        public Task<Exercice> GetExerciceById(string exerciceId);
        public Task<List<Exercice>> GetExerciceUserId(string userId);
        public Task<List<Exercice>> GetExercicePublic();
        public Task<bool> PostExercice(Exercice exercice);
        public Task<bool> UpdateExercice(string exerciceId, string name);
        public Task<bool> Delete(string exerciceId);
    }
}
