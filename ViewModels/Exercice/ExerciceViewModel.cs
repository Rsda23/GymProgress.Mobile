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
            DisplayExercice();
        }

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool hasExercice;
        [ObservableProperty]
        private bool emptyExercice;

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
        private async Task DisplayExercice()
        {
            var exercices = await _exercicesService.GetAllExercice();

            if (exercices != null)
            {
                Exercices.Clear();
                foreach (var exercice in exercices)
                {
                    Exercices.Add(exercice);
                }
            }

            FilterExercices = new ObservableCollection<Exercice>(Exercices);

            VisibleExercice();
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
            HasExercice = Exercices.Any();
            EmptyExercice = !HasExercice;
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
    }
}
