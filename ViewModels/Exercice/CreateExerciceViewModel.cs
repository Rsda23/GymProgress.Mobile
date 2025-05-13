using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.ViewModels.SnackBar;

namespace GymProgress.Mobile.ViewModels
{
    public partial class CreateExerciceViewModel : ViewModelBase
    {
        private readonly IExercicesService _exercicesService;
        private readonly SnackBarViewModel _snackBar;
        public CreateExerciceViewModel(IExercicesService exercicesService, SnackBarViewModel snackBar)
        {
            _exercicesService = exercicesService;
            _snackBar = snackBar;
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
            IsRunning = true;

            try
            {
                string userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("L'id de l'utilisateur est vide");
                }
                if (string.IsNullOrWhiteSpace(NameExerciceText))
                {
                    throw new Exception("Le nom de l'exercice est vide");
                }

                Exercice exercice = new Exercice(NameExerciceText, userId);

                await _exercicesService.PostExercice(exercice);
                await Shell.Current.GoToAsync($"///{Routes.ExercicePage}");

                _snackBar.Succefull("Exercice créé !");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "fermer");
            }
            finally
            {
                IsRunning = false;
            }
        }
    }
}
