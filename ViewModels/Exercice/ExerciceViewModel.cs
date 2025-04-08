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
        private bool hasExercice;
        [ObservableProperty]
        private bool emptyExercice;

        [ObservableProperty]
        private string buttonCreateExerciceText = "Créer";

        [ObservableProperty]
        private string titleExerciceText = "Exercice";

        [ObservableProperty]
        private ObservableCollection<Exercice> exercices = new();



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
                foreach (var exercice in exercices)
                {
                    Exercices.Add(exercice);
                }
            }

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
    }
}
