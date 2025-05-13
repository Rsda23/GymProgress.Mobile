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
        private ObservableCollection<Seance> seances = new();

        [ObservableProperty]
        private ObservableCollection<Seance> filterSeances = new();

        [ObservableProperty]
        private Seance? selectedSeance;



        [RelayCommand]
        private async Task SelectSeance(Seance model)
        {
            IsRunning = true;

            try
            {
                if (model == null)
                {
                    throw new Exception("Le model est null");
                }

                string test = model.SeanceId;
                ShellNavigationQueryParameters parameters = new ShellNavigationQueryParameters();
                parameters.Add(Constants.QueryIdentifiers.SeanceId, model.SeanceId);

                await Shell.Current.GoToAsync($"/{Routes.SeanceDetailPage}", parameters);

                Deselect();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "fermer");
            }
            finally
            {
                IsRunning = false;
            }
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
            if (Seances.Any())
            {
                HasSeance = true;
            }
            else
            {
                HasSeance = false;
            }
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
            IsRunning = true;

            try
            {
                string userId = Preferences.Get("UserId", string.Empty);
                if(string.IsNullOrEmpty(userId))
                {
                    throw new Exception("L'id de l'utilisateur est introuvable");
                }

                List<Seance>? seancesUser = await _seanceService.GetSeanceByUserId(userId);

                if (seancesUser != null)
                {
                    Seances.Clear();
                    foreach (Seance seance in seancesUser)
                    {
                        Seances.Add(seance);
                    }
                }

                FilterSeances = new ObservableCollection<Seance>(Seances);

                await VisibleSeance();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "fermer");
            }
            finally
            {
                IsRunning = false;
            }
        }

        public void Deselect()
        {
            SelectedSeance = null;
        }
    }
}
