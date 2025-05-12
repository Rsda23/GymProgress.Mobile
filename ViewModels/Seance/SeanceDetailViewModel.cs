using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Core;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.ViewModels.SnackBar;

namespace GymProgress.Mobile.ViewModels
{
    [QueryProperty(Constants.TargetProperties.SeanceId, Constants.QueryIdentifiers.SeanceId)]
    public partial class SeanceDetailViewModel : ViewModelBase
    {
        private readonly ISeancesService _seanceService;
        private readonly IExercicesService _exerciceService;
        private readonly SnackBarViewModel _snackBar;
        public SeanceDetailViewModel(ISeancesService service, SnackBarViewModel snackBar, IExercicesService exerciceService)
        {
            _seanceService = service;
            _snackBar = snackBar;
            _exerciceService = exerciceService;
        }

        [ObservableProperty]
        private Seance currentSeance = new();

        [ObservableProperty]
        private List<Exercice> exercices = [];

        [ObservableProperty]
        private string seanceName = string.Empty;

        [ObservableProperty]
        private string seanceId = string.Empty;

        [ObservableProperty]
        private bool confirm = false;

        [ObservableProperty]
        private string emptyExerciceText = "Aucun Exercice";

        [ObservableProperty]
        private bool hasExercice;

        [ObservableProperty]
        private bool isLoaded;

        [ObservableProperty]
        private Exercice selectedExercice;



        [RelayCommand]
        private async Task Delete()
        {
            Confirm = await Shell.Current.DisplayAlert(
                "Confirmation",
                "Voulez-vous vraiment supprimer cet exercice ?",
                "Oui", "Non");

            if (Confirm)
            {
                await _seanceService.Delete(currentSeance.SeanceId);
                await Shell.Current.GoToAsync("..");

                _snackBar.Succefull("Suppression effectuée !");
            }
        }

        [RelayCommand]
        private async Task AddExercice()
        {
            var seance = SeanceId;
            ShellNavigationQueryParameters parameters = new ShellNavigationQueryParameters();
            parameters.Add(Constants.QueryIdentifiers.SeanceId, seance);

            await Shell.Current.GoToAsync($"/{Routes.AddExercicePage}", parameters);
        }

        [RelayCommand]
        private Task VisibleSeance()
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

        async partial void OnSeanceIdChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                CurrentSeance = await _seanceService.GetSeanceById(value);

                await DisplayExercice();
                await VisibleSeance();
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

        [RelayCommand]
        private async Task Remove(Exercice exercice)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmation", "Êtes-vous sûr de vouloir retirer cet exercice ?", "Oui", "Non");
            if (confirm)
            {
                await _seanceService.RemoveExerciceToSeance(CurrentSeance.SeanceId, exercice.ExerciceId);

                _snackBar.Succefull("Exercice retiré !");

                await DisplayExercice();
            }
        }


        public void Deselect()
        {
            SelectedExercice = null;
        }

        public async Task DisplayExercice()
        {
            IsRunning = true;
            IsLoaded = false;

            List<Exercice> exercices = await _exerciceService.GetExercicesBySeanceId(CurrentSeance.SeanceId);

            if (exercices != null)
            {
                Exercices.Clear();
                foreach (var exercice in exercices)
                {
                    Exercices.Add(exercice);
                }
            }

            IsLoaded = true;
            IsRunning = false;
        }
    }
}
