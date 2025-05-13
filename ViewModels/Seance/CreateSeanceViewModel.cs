using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.ViewModels.SnackBar;

namespace GymProgress.Mobile.ViewModels
{
    public partial class CreateSeanceViewModel : ViewModelBase
    {
        private readonly IExercicesService _exercicesService;
        private readonly ISeancesService _seancesService;
        private readonly SnackBarViewModel _snackbar;
        public CreateSeanceViewModel(IExercicesService exercicesService, ISeancesService seancesService, SnackBarViewModel snackBar)
        {
            _exercicesService = exercicesService;
            _seancesService = seancesService;
            _snackbar = snackBar;
        }

        [ObservableProperty]
        private string nameSeanceText = string.Empty;

        [ObservableProperty]
        private string errorSeanceText = string.Empty;

        [ObservableProperty]
        private bool errorSeance = false;



        [RelayCommand]
        private async Task ButtonValide()
        {
            IsRunning = true;

            try
            {
                if (!string.IsNullOrWhiteSpace(NameSeanceText))
                {
                    string userId = Preferences.Get("UserId", string.Empty);
                    if (string.IsNullOrEmpty(userId))
                    {
                        throw new Exception("L'id de l'utilisateur est vide");
                    }

                    Seance seance = new Seance(NameSeanceText, userId);

                    if (seance != null)
                    {
                        ErrorSeance = false;

                        await _seancesService.PostSeance(seance);
                        await Shell.Current.GoToAsync($"///{Routes.SeancePage}");

                        _snackbar.Succefull("Séance créé !");
                    }
                    else
                    {
                        throw new Exception("La seance est nulle");
                    }
                }
                else
                {
                    Exception ex = new Exception("Le champs ne peut pas être vide.");
                    ErrorSeanceText = ex.Message;
                    ErrorSeance = true;
                }
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
