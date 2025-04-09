using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Core;
using GymProgress.Mobile.Interfaces;
using System.Collections.ObjectModel;

namespace GymProgress.Mobile.ViewModels
{
    [QueryProperty(Constants.TargetProperties.SeanceName, Constants.QueryIdentifiers.SeanceName)]
    public partial class AddExerciceViewModel : ViewModelBase
    {
        private readonly IExercicesService _exercicesService;
        public AddExerciceViewModel(IExercicesService exercicesService)
        {
            _exercicesService = exercicesService;
            DisplayExercice();
        }

        [ObservableProperty]
        private string buttonValidExerciceText = "Confirmer";

        [ObservableProperty]
        private string buttonCreateExerciceText = "Créer";

        [ObservableProperty]
        private string seanceName = string.Empty;

        [ObservableProperty]
        private bool isSelected;

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
        }

        [RelayCommand]
        private async Task ButtonValidExercice()
        {
            var seance = SeanceName;
            ShellNavigationQueryParameters parameters = new ShellNavigationQueryParameters();
            parameters.Add(Constants.QueryIdentifiers.SeanceName, seance);

            await Shell.Current.GoToAsync($"/{Routes.SeanceDetailPage}", parameters);
        }
    }
}
