using CommunityToolkit.Maui.Converters;
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

            try
            {
                List<Exercice>? exercicesPublic = await _exercicesService.GetExercicePublic();

                string userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("L'id de l'utilisateur est vide");
                }
                List<Exercice>? exercicesUser = await _exercicesService.GetExerciceUserId(userId);

                if (exercicesUser != null)
                {
                    foreach (Exercice exercice in exercicesUser)
                    {
                        Exercices.Add(new ExerciceSelectableViewModel { Exercice = exercice });
                    }
                }

                if (exercicesPublic != null)
                {
                    foreach (Exercice exercice in exercicesPublic)
                    {
                        Exercices.Add(new ExerciceSelectableViewModel { Exercice = exercice });
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "fermer");
            }
            finally
            {
                IsLoaded = true;
                IsRunning = false;
            }
        }

        [RelayCommand]
        private async Task ButtonValidExercice()
        {
            IsRunning = true;

            try
            {
                ExercicesId.Clear();

                if (!Exercices.Any())
                {
                    throw new Exception("Aucun exercices trouvé");
                }

                foreach (ExerciceSelectableViewModel exercice in Exercices.Where(e => e.IsSelected))
                {
                    ExercicesId.Add(exercice.Id);
                }

                if (!string.IsNullOrEmpty(CurrentSeance.SeanceId) && ExercicesId.Any())
                {
                    await _seanceService.AddExerciceToSeanceById(CurrentSeance.SeanceId, ExercicesId.ToList());
                }
                else
                {
                    throw new Exception("L'id de la seance est vide ou aucune exercice selectionné");
                }

                await GoToSeanceDetail();

                _snackBar.Succefull("Exercice ajouté !");
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



        async partial void OnSeanceIdChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Seance? seance = await _seanceService.GetSeanceById(value);
                
                if (seance != null)
                {
                    CurrentSeance = seance;
                    await DisplayExercice();
                }
            }
        }

        private async Task  GoToSeanceDetail()
        {
            await Shell.Current.GoToAsync($"//{Routes.SeancePage}");
            await Shell.Current.GoToAsync($"{Routes.SeanceDetailPage}?{Constants.QueryIdentifiers.SeanceId}={SeanceId}");
        }
    }
}
