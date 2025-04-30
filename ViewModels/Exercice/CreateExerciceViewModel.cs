using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;

namespace GymProgress.Mobile.ViewModels
{
    public partial class CreateExerciceViewModel : ViewModelBase
    {
        private readonly IExercicesService _exercicesService;
        public CreateExerciceViewModel(IExercicesService exercicesService)
        {
            _exercicesService = exercicesService;
        }
        [ObservableProperty]
        private string buttonAddExerciceText = "Ajouter";

        [ObservableProperty]
        private string buttonValideText = "Valider";

        [ObservableProperty]
        private string nameExerciceText = string.Empty;



        [RelayCommand]
        private async Task ButtonAddExercice()
        {
            await Shell.Current.GoToAsync("AddExercicePage");
        }

        [RelayCommand]
        private async Task ButtonValide()
        {
            string userId = Preferences.Get("UserId", string.Empty);
            Exercice exercice = new Exercice(NameExerciceText, userId);

            await _exercicesService.PostExercice(exercice);
            await Shell.Current.GoToAsync($"///{Routes.ExercicePage}");
        }
    }
}
