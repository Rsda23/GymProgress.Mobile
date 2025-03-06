using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System.Collections.ObjectModel;

namespace GymProgress.Mobile.ViewModels
{
    public partial class SeanceViewModel : ViewModelBase
    {
        private readonly ISeancesService _seanceService;

        public SeanceViewModel(ISeancesService seanceService)
        {
            _seanceService = seanceService;
            DisplaySeance();
        }

        [ObservableProperty]
        private string buttonCreateSeanceText = "Créer";

        [ObservableProperty]
        private string titleSeance = "Séance";

        [ObservableProperty]
        private bool hasSeance;

        [ObservableProperty]
        private bool emptySeance;

        [ObservableProperty]
        private ObservableCollection<Seance> seances = new();


        [RelayCommand]
        private async Task DisplaySeance()
        {
            var seances = await _seanceService.GetAllSeance();
            if (seances != null)
            {
                foreach (var seance in seances)
                {
                    Seances.Add(seance);
                }
            }

            VisibleSeance();
        }

        [RelayCommand]
        private async Task ButtonCreateSeance()
        {
            await Shell.Current.GoToAsync($"/{Routes.SeanceDetailPage}");
        }

        [RelayCommand]
        private async Task ButtonGoToSeance()
        {
            //var seance = await _seanceService.GetSeanceByName("pull");
            await Shell.Current.GoToAsync($"/{Routes.SeancePage}");
        }

        [RelayCommand]
        private Task VisibleSeance()
        {
            HasSeance = seances.Any();
            EmptySeance = !HasSeance;
            return Task.CompletedTask;
        }
    }
}
