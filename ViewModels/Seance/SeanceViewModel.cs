using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Core;
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
        }

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private string buttonCreateSeanceText = "Créer";

        [ObservableProperty]
        private string titleSeance = "Séance";

        [ObservableProperty]
        private string emptySeanceText = "Aucune séance";

        [ObservableProperty]
        private bool hasSeance;
        [ObservableProperty]
        private bool emptySeance;

        [ObservableProperty]
        private ObservableCollection<Seance> seances = new();

        [ObservableProperty]
        private ObservableCollection<Seance> filterSeances = new();

        [ObservableProperty]
        private Seance selectedSeance;



        [RelayCommand]
        private async Task SelectSeance(Seance model)
        {
            var test = model.Name;
            ShellNavigationQueryParameters parameters = new ShellNavigationQueryParameters();
            parameters.Add(Constants.QueryIdentifiers.SeanceName, model.Name);
    
            await Shell.Current.GoToAsync($"/{Routes.SeanceDetailPage}", parameters);

            Deselect();
        }

        [RelayCommand]
        private async Task ButtonCreateSeance()
        {
            await Shell.Current.GoToAsync($"/{Routes.CreateSeancePage}");
        }

        [RelayCommand]
        private async Task ButtonGoToSeance()
        {
            await Shell.Current.GoToAsync($"/{Routes.SeanceDetailPage}");
        }

        [RelayCommand]
        private Task VisibleSeance()
        {
            HasSeance = Seances.Any();
            EmptySeance = !HasSeance;
            return Task.CompletedTask;
        }

        [RelayCommand]
        private void FilterSeancesBySearchText(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                FilterSeances = new ObservableCollection<Seance>(Seances);
            }
            else
            {
                FilterSeances = new ObservableCollection<Seance>(Seances .Where(
                    seance => !string.IsNullOrEmpty(seance.Name) && 
                    seance.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)));
            }
        }



        public async Task DisplaySeance()
        {
            var seances = await _seanceService.GetAllSeance();

            if (seances != null)
            {
                Seances.Clear();
                foreach (var seance in seances)
                {
                    Seances.Add(seance);
                }
            }

            FilterSeances = new ObservableCollection<Seance>(Seances);

            VisibleSeance();
        }

        public void Deselect()
        {
            SelectedSeance = null;
        }
    }
}
