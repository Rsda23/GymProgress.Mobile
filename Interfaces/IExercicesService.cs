using GymProgress.Domain.Models;

namespace GymProgress.Mobile.Interfaces
{
    public interface IExercicesService
    {
        public Task<List<Exercice>> GetAllExercice();
        public Task<Exercice> GetExerciceByName(string name);
        public Task<bool> PostExercice(string name);
        public Task<bool> Delete(string exerciceId);
    }
}
