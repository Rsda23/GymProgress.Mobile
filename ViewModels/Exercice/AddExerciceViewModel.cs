using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Core;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.ViewModels.SnackBar;
using System.Collections.ObjectModel;

namespace GymProgress.Mobile.ViewModels
{
    [QueryProperty(Constants.TargetProperties.SeanceId, Constants.QueryIdentifiers.SeanceId)]
    public partial class AddExerciceViewModel : ViewModelBase
    {
        private readonly IExercicesService _exercicesService;
        private readonly ISeancesService _seanceService;
        private readonly SnackBarViewModel _snackBar;
        public AddExerciceViewModel(IExercicesService exercicesService, ISeancesService seancesService, SnackBarViewModel snackBar)
        {
            _exercicesService = exercicesService;
            _seanceService = seancesService;
            _snackBar = snackBar;
        }

        [ObservableProperty]
        private Seance currentSeance = new();

        [ObservableProperty]
        private string buttonValidExerciceText = "Confirmer";

        [ObservableProperty]
        private string buttonCreateExerciceText = "Créer";

        [ObservableProperty]
        private string seanceId = string.Empty;

        [ObservableProperty]
        private bool isSelected;

        [ObservableProperty]
        private bool isLoaded;

        [ObservableProperty]
        private ObservableCollection<ExerciceSelectableViewModel> exercices = new();

        [ObservableProperty]
        private ObservableCollection<string> exercicesId = new();



        [RelayCommand]
        private async Task ButtonCreateExercice()
        {
            await Shell.Current.GoToAsync("CreateExercicePage");
        }

        [RelayCommand]
        private async Task DisplayExercice()
        {
            IsRunning = true;
            IsLoaded = false;

            List<Exercice> exercicesPublic = await _exercicesService.GetExercicePublic();

            string userId = Preferences.Get("UserId", string.Empty);
            List<Exercice> exercicesUser = await _exercicesService.GetExerciceUserId(userId);
            
            if (exercicesUser != null)
            {
                foreach (var exercice in exercicesUser)
                {
                    Exercices.Add(new ExerciceSelectableViewModel { Exercice = exercice });
                }
                foreach (var exercice in exercicesPublic)
                {
                    Exercices.Add(new ExerciceSelectableViewModel { Exercice = exercice });
                }
            }

            IsLoaded = true;
            IsRunning = false;
        }

        [RelayCommand]
        private async Task ButtonValidExercice()
        {
            IsRunning = true;

            ExercicesId.Clear();

            foreach (var exercice in Exercices.Where(e => e.IsSelected))
            {
                ExercicesId.Add(exercice.Id);
            }
           
            await _seanceService.AddExerciceToSeanceById(CurrentSeance.SeanceId, ExercicesId.ToList());

            await GoToSeanceDetail();

            _snackBar.Succefull("Exercice ajouté !");

            IsRunning = false;
        }



        async partial void OnSeanceIdChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                CurrentSeance = await _seanceService.GetSeanceById(value);
                await DisplayExercice();
            }
        }

        private async Task  GoToSeanceDetail()
        {
            await Shell.Current.GoToAsync($"//{Routes.SeancePage}");
            await Shell.Current.GoToAsync($"{Routes.SeanceDetailPage}?{Constants.QueryIdentifiers.SeanceId}={SeanceId}");
        }
    }
}
