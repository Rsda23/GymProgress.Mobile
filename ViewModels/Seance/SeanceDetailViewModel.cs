using CommunityToolkit.Mvvm.ComponentModel;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Core;
using GymProgress.Mobile.Interfaces;

namespace GymProgress.Mobile.ViewModels
{
    [QueryProperty(Constants.TargetProperties.SeanceName, Constants.QueryIdentifiers.SeanceName)]
    public partial class SeanceDetailViewModel : ViewModelBase
    {
        private readonly ISeancesService _service;

        [ObservableProperty]
        private Seance currentSeance = new();

        [ObservableProperty]
        private List<Exercice> exercices = [];

        [ObservableProperty]
        private string seanceName = string.Empty;

        async partial void OnSeanceNameChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                CurrentSeance = await _service.GetSeanceByName(value);
                Exercices = CurrentSeance.Exercices;
            }
        }

        public SeanceDetailViewModel(ISeancesService service)
        {
            _service = service;
        }
    }
}
