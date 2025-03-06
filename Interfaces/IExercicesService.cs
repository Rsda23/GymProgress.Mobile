using GymProgress.Domain.Models;

namespace GymProgress.Mobile.Interfaces
{
    public interface IExercicesService
    {
        public Task<List<Exercice>> GetAllExercice();
    }
}
