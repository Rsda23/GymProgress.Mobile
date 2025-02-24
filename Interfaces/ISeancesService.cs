using GymProgress.Domain.Models;

namespace GymProgress.Mobile.Interfaces
{
    public interface ISeancesService
    {
        Task<Seance> GetSeanceByName(string name);
    }
}
