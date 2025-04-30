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
        private readonly ISeancesService _seanceService;
        public AddExerciceViewModel(IExercicesService exercicesService, ISeancesService seancesService)
        {
            _exercicesService = exercicesService;
            _seanceService = seancesService;
            DisplayExercice();
        }

        [ObservableProperty]
        private Seance currentSeance = new();

        [ObservableProperty]
        private string buttonValidExerciceText = "Confirmer";

        [ObservableProperty]
        private string buttonCreateExerciceText = "Créer";

        [ObservableProperty]
        private string seanceName = string.Empty;

        [ObservableProperty]
        private bool isSelected;

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
        }

        [RelayCommand]
        private async Task ButtonValidExercice()
        {
            ExercicesId.Clear();

            foreach (var exercice in Exercices.Where(e => e.IsSelected))
            {
                ExercicesId.Add(exercice.Id);
            }

            await _seanceService.AddExerciceToSeanceById(CurrentSeance.SeanceId, ExercicesId.ToList());

            await GoToSeanceDetail();

        }



        async partial void OnSeanceNameChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                CurrentSeance = await _seanceService.GetSeanceByName(value);
            }
        }

        private async Task  GoToSeanceDetail()
        {
            await Shell.Current.GoToAsync($"//{Routes.SeancePage}");
            await Shell.Current.GoToAsync($"{Routes.SeanceDetailPage}?{Constants.QueryIdentifiers.SeanceName}={SeanceName}");
        }
    }
}
