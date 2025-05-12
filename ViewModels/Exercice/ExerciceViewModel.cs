using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Core;
using GymProgress.Mobile.Interfaces;
using System.Collections.ObjectModel;

namespace GymProgress.Mobile.ViewModels
{
    public partial class ExerciceViewModel : ViewModelBase
    {
        private readonly IExercicesService _exercicesService;
        public ExerciceViewModel(IExercicesService exercicesService)
        {
            _exercicesService = exercicesService;
        }

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool hasExercice = true;

        [ObservableProperty]
        private bool isLoaded;

        [ObservableProperty]
        private string buttonCreateExerciceText = "Créer";

        [ObservableProperty]
        private string titleExerciceText = "Exercice";

        [ObservableProperty]
        private ObservableCollection<Exercice> exercices = new();

        [ObservableProperty]
        private ObservableCollection<Exercice> filterExercices = new();



        [RelayCommand]
        private async Task ButtonCreateExercice()
        {
            await Shell.Current.GoToAsync("CreateExercicePage");
        }

        [RelayCommand]
        private async Task SelectExercice(Exercice model)
        {
            var test = model.Nom;
            ShellNavigationQueryParameters parameters = new ShellNavigationQueryParameters();
            parameters.Add(Constants.QueryIdentifiers.ExerciceNom, model.Nom);

            await Shell.Current.GoToAsync($"/{Routes.ExerciceDetailPage}", parameters);
        }

        [RelayCommand]
        private Task VisibleExercice()
        {
            if (Exercices.Any())
            {
                HasExercice = true;
            }
            else
            {
                HasExercice = false;
            }
            return Task.CompletedTask;
        }

        [RelayCommand]
        private void FilterExercicesBySearch(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                FilterExercices = new ObservableCollection<Exercice>(Exercices);
            }
            else
            {
                FilterExercices = new ObservableCollection<Exercice>(Exercices.Where(
                    exercice => !string.IsNullOrEmpty(exercice.Nom) &&
                    exercice.Nom.Contains(searchText, StringComparison.OrdinalIgnoreCase)));
            }
        }


        public async Task DisplayExercice()
        {
            IsRunning = true;
            IsLoaded = false;

            List<Exercice> exercicesPublic = await _exercicesService.GetExercicePublic();

            string userId = Preferences.Get("UserId", string.Empty);
            List<Exercice> exercicesUser = await _exercicesService.GetExerciceUserId(userId);

            if (exercicesUser != null && exercicesPublic != null)
            {
                Exercices.Clear();
                foreach (Exercice exercice in exercicesUser)
                {
                    Exercices.Add(exercice);
                }

                foreach (var exercice in exercicesPublic)
                {
                    Exercices.Add(exercice);
                }
            }

            FilterExercices = new ObservableCollection<Exercice>(Exercices);

            await VisibleExercice();

            IsLoaded = true;
            IsRunning = false;
        }
    }
}
