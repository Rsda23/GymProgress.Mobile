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
        private Exercice? selectedExercice;



        [RelayCommand]
        private async Task Delete()
        {
            IsRunning = true;

            try
            {
                Confirm = await Shell.Current.DisplayAlert(
                "Confirmation",
                "Voulez-vous vraiment supprimer cet exercice ?",
                "Oui", "Non");

                if (Confirm)
                {
                    if (string.IsNullOrEmpty(CurrentSeance.SeanceId))
                    {
                        throw new Exception("L'id de la seance en cours est vide");
                    }

                    await _seanceService.Delete(CurrentSeance.SeanceId);
                    await Shell.Current.GoToAsync("..");

                    _snackBar.Succefull("Suppression effectuée !");
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

        [RelayCommand]
        private async Task AddExercice()
        {
            IsRunning = true;

            try
            {
                if (string.IsNullOrEmpty(SeanceId))
                {
                    throw new Exception("L'id de la seance est vide");
                }

                string seance = SeanceId;
                ShellNavigationQueryParameters parameters = new ShellNavigationQueryParameters();
                parameters.Add(Constants.QueryIdentifiers.SeanceId, seance);

                await Shell.Current.GoToAsync($"/{Routes.AddExercicePage}", parameters);
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
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Seance? seance = await _seanceService.GetSeanceById(value);

                    if (seance != null)
                    {
                        CurrentSeance = seance;

                        await DisplayExercice();
                        await VisibleSeance();
                    }
                    else
                    {
                        throw new Exception("La seances est nulle");
                    }
                }
                else
                {
                    throw new Exception("La value est nulle");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "fermer");
            }
        }

        [RelayCommand]
        private async Task SelectExercice(Exercice model)
        {
            IsRunning = true;

            try
            {
                if (model == null)
                {
                    throw new Exception("Le model est null");
                }

                string test = model.Nom;
                ShellNavigationQueryParameters parameters = new ShellNavigationQueryParameters();
                parameters.Add(Constants.QueryIdentifiers.ExerciceNom, model.Nom);

                await Shell.Current.GoToAsync($"/{Routes.ExerciceDetailPage}", parameters);

                Deselect();
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

        [RelayCommand]
        private async Task Remove(Exercice exercice)
        {
            IsRunning = true;

            try
            {
                bool confirm = await Shell.Current.DisplayAlert("Confirmation", "Êtes-vous sûr de vouloir retirer cet exercice ?", "Oui", "Non");

                if (confirm)
                {
                    if (exercice == null)
                    {
                        throw new Exception("L'exercice est null");
                    }
                    if (string.IsNullOrEmpty(CurrentSeance.SeanceId))
                    {
                        throw new Exception("L'id de la seance en cours est vide");
                    }

                    await _seanceService.RemoveExerciceToSeance(CurrentSeance.SeanceId, exercice.ExerciceId);

                    _snackBar.Succefull("Exercice retiré !");

                    await DisplayExercice();
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


        public void Deselect()
        {
            SelectedExercice = null;
        }

        public async Task DisplayExercice()
        {
            IsRunning = true;
            IsLoaded = false;

            if (CurrentSeance == null || string.IsNullOrEmpty(CurrentSeance.SeanceId))
            {
                Console.WriteLine("Pas encore initialisé");
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(CurrentSeance.SeanceId))
                {
                    throw new Exception("L'id de la seance en cours est vide");
                }

                List<Exercice>? exercices = await _exerciceService.GetExercicesBySeanceId(CurrentSeance.SeanceId);

                if (exercices != null)
                {
                    Exercices.Clear();

                    foreach (Exercice exercice in exercices)
                    {
                        Exercices.Add(exercice);
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
    }
}
