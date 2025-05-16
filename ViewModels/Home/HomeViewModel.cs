using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using System.Collections.ObjectModel;

namespace GymProgress.Mobile.ViewModels.Home
{
    public partial class HomeViewModel : ViewModelBase
    {
        private readonly ISeancesService _seancesService;
        public HomeViewModel(ISeancesService seancesService)
        {
            _seancesService = seancesService;
        }

        [ObservableProperty]
        private string pseudo = string.Empty;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private int countSeance;

        [ObservableProperty]
        private bool isLoaded;

        [ObservableProperty]
        private string titleSeance = string.Empty;

        [ObservableProperty]
        private bool hasSeance;

        [ObservableProperty]
        private ObservableCollection<Seance> seances = new();
        [ObservableProperty]
        private ObservableCollection<Seance> lastSeances = new();



        [RelayCommand]
        public async Task DisplayLastSeances()
        {
            IsRunning = true;
            IsLoaded = false;

            try
            {
                string userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("L'id de l'utilisateur est vide");
                }
             
                List<Seance>? lastSeances = await _seancesService.GetLastSeance(3, userId);
                List<Seance>? seances = await _seancesService.GetSeanceByUserId(userId);

                if (lastSeances != null)
                {
                    LastSeances.Clear();
                    foreach (Seance seance in lastSeances)
                    {
                        LastSeances.Add(seance);
                    }
                }
                if (seances != null)
                {
                    Seances.Clear();
                    foreach (Seance seance in seances)
                    {
                        Seances.Add(seance);
                    }

                    CountSeance = Seances.Count();

                    TitleSeance = $"Nombre total de seances : {CountSeance}";
                }

                await VisibleSeance();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "fermer");
            }
            finally
            {
                IsLoaded = true;
                IsRunning = false;
            }
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
    }
}
