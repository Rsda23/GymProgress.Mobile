using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Core;
using GymProgress.Mobile.Interfaces;

namespace GymProgress.Mobile.ViewModels
{
    [QueryProperty(Constants.TargetProperties.SeanceName, Constants.QueryIdentifiers.SeanceName)]
    public partial class SeanceDetailViewModel : ViewModelBase
    {
        private readonly ISeancesService _seanceService;
        private readonly IExercicesService _exerciceService;
        public SeanceDetailViewModel(ISeancesService service)
        {
            _seanceService = service;
        }

        [ObservableProperty]
        private Seance currentSeance = new();

        [ObservableProperty]
        private List<Exercice> exercices = [];

        [ObservableProperty]
        private string seanceName = string.Empty;

        [ObservableProperty]
        private string buttonAddExerciceText = "Ajouter";

        [ObservableProperty]
        private string emptyExerciceText = "Aucun Exercice";

        [ObservableProperty]
        private bool hasExercice;
        [ObservableProperty]
        private bool emptyExercice;

        [ObservableProperty]
        private Exercice selectedExercice;



        [RelayCommand]
        private async Task ButtonAddExercice()
        {
            var seance = SeanceName;
            ShellNavigationQueryParameters parameters = new ShellNavigationQueryParameters();
            parameters.Add(Constants.QueryIdentifiers.SeanceName, seance);

            await Shell.Current.GoToAsync($"/{Routes.AddExercicePage}", parameters);
        }

        [RelayCommand]
        private Task VisibleSeance()
        {
            HasExercice = Exercices.Any();
            EmptyExercice = !HasExercice;
            return Task.CompletedTask;
        }

        async partial void OnSeanceNameChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                CurrentSeance = await _seanceService.GetSeanceByName(value);
                Exercices = CurrentSeance.Exercices;
                VisibleSeance();
            }
        }

        [RelayCommand]
        private async Task SelectExercice(Exercice model)
        {
            var test = model.Nom;
            ShellNavigationQueryParameters parameters = new ShellNavigationQueryParameters();
            parameters.Add(Constants.QueryIdentifiers.ExerciceNom, model.Nom);

            await Shell.Current.GoToAsync($"/{Routes.ExerciceDetailPage}", parameters);

            Deselect();
        }



        public void Deselect()
        {
            SelectedExercice = null;
        }
    }
}
