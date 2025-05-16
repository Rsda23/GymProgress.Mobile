using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Core;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.View.Popups;
using GymProgress.Mobile.ViewModels.Popups;
using GymProgress.Mobile.ViewModels.SnackBar;
using System.Collections.ObjectModel;

namespace GymProgress.Mobile.ViewModels
{
    [QueryProperty(Constants.TargetProperties.ExerciceId, Constants.QueryIdentifiers.ExerciceId)]
    [QueryProperty(Constants.TargetProperties.SeanceId, Constants.QueryIdentifiers.SeanceId)]
    public partial class ExerciceDetailViewModel : ViewModelBase
    {
        private readonly IExercicesService _service;
        private readonly ISetDatasService _setDatasService;
        private readonly ISeancesService _seanceService;
        private readonly SnackBarViewModel _snackBar;
        public ExerciceDetailViewModel(IExercicesService service, ISetDatasService setDatasService, ISeancesService seancesService, SnackBarViewModel snackBar)
        {
            _service = service;
            _setDatasService = setDatasService;
            _seanceService = seancesService;
            _snackBar = snackBar;
        }

        [ObservableProperty]
        private Exercice currentExercice = new();

        [ObservableProperty]
        private Seance currentSeance = new();

        [ObservableProperty]
        private string exerciceNom = string.Empty;

        [ObservableProperty]
        private string exerciceId = string.Empty;

        [ObservableProperty]
        private string seanceId = string.Empty;

        [ObservableProperty]
        private DateTime date = DateTime.Now;

        [ObservableProperty]
        private bool confirm = false;

        [ObservableProperty]
        private bool isEditing = false;

        [ObservableProperty]
        public bool hasPublic = true;

        [ObservableProperty]
        private bool hasSetData;

        [ObservableProperty]
        private bool isLoaded;

        [ObservableProperty]
        private ObservableCollection<SetData> setDatas = new();


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
                    if (string.IsNullOrEmpty(CurrentExercice.ExerciceId))
                    {
                        throw new Exception("L'id de l'exercice est vide");
                    }

                    await _service.Delete(CurrentExercice.ExerciceId);
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
        private void Editing()
        {
            IsEditing = !IsEditing;
        }

        [RelayCommand]
        private async Task SaveEdit()
        {
            if (!SetDatas.Any())
            {
                throw new Exception("Aucune Serie trouvé");
            }

            foreach (SetData setData in SetDatas)
            {
                await _setDatasService.ReplaceSetData(setData);
            }

            if (!string.IsNullOrEmpty(CurrentExercice.ExerciceId) && !string.IsNullOrEmpty(CurrentExercice.Nom))
            {
                await _service.UpdateExercice(CurrentExercice.ExerciceId, CurrentExercice.Nom);
                
                IsEditing = false;

                await DisplayByExerciceId(CurrentExercice.ExerciceId);
            }
            else
            {
                throw new Exception("L'id ou le nom de l'exercice est vide");
            }
        }

        [RelayCommand]
        private async Task AddSetData()
        {
            IsRunning = true; 

            try
            {
                if (!string.IsNullOrEmpty(ExerciceId))
                {
                    Exercice? exercice = await _service.GetExerciceById(ExerciceId);
                    if (exercice == null)
                    {
                        throw new Exception("l'exercice est vide");
                    }

                    if (!string.IsNullOrEmpty(exercice.ExerciceId) && exercice != null)
                    {
                        AddSetDataPopupViewModel viewModel = new AddSetDataPopupViewModel(_setDatasService, exercice.ExerciceId, _snackBar);
                        AddSetDataPopup popup = new AddSetDataPopup(viewModel);
                        await Shell.Current.CurrentPage.ShowPopupAsync(popup);
                    }
                    else
                    {
                        throw new Exception("Exercice introuvable ou l'id de l'exercice est vide");
                    }
                }
                else
                {
                    throw new Exception("Nom de l'exercice introuvable");
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
        private async Task DeleteSetData(SetData setData)
        {
            IsRunning = true;

            try
            {
                bool confirm = await Shell.Current.DisplayAlert("Confirmation", "Êtes-vous sûr de vouloir suprrimer cette série ?", "Oui", "Non");

                if (confirm)
                {
                    if (setData != null)
                    {
                        await _setDatasService.Delete(setData.SetDataId);
                        await DisplaySetData();

                        _snackBar.Succefull("Suppression effectuée !");
                    }
                    else
                    {
                        throw new Exception("SetData est null");
                    }
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



        async partial void OnExerciceIdChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Exercice? exercice = await _service.GetExerciceById(value);
                if (exercice != null)
                {
                    CurrentExercice = exercice;
                    ExerciceNom = CurrentExercice.Nom;
                }
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
                }
            }
        }

        public async Task DisplaySetData()
        {
            IsRunning = true;
            IsLoaded = false;

            try
            {
                Exercice? exercice = await _service.GetExerciceById(ExerciceId);

                if (exercice == null)
                {
                    throw new Exception("Auncun exercice trouvé");
                }

                string userId = Preferences.Get("UserId", string.Empty);

                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("L'id de l'utilisateur est vide");
                }

                List<SetData>? setDatas = await _setDatasService.GetSetDataByExerciceAndUser(exercice.ExerciceId, userId);
                if (setDatas == null)
                {
                    throw new Exception("La serie est null");
                }

                if (setDatas.Count() != 0)
                {
                    SetDatas.Clear();

                    HasSetData = true;

                    foreach (SetData setData in setDatas)
                    {
                        SetDatas.Add(setData);
                    }
                }
                else
                {
                    HasSetData = false;
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

        public async Task DisplayAddByName()
        {
            IsRunning = true;
            IsLoaded = false;

            try
            {
                Exercice? exercice = await _service.GetExerciceByName(ExerciceNom);
                if (exercice == null)
                {
                    throw new Exception("Auncun exercice trouvé");
                }

                string userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("L'id de l'utilisateur est vide");
                }

                List<SetData>? setDatas = await _setDatasService.GetSetDataByExerciceAndUser(exercice.ExerciceId, userId);
                if (setDatas == null)
                {
                    throw new Exception("La serie est null");
                }

                if (setDatas.Count() != 0)
                {
                    SetDatas.Clear();

                    HasSetData = true;

                    foreach (SetData setData in setDatas)
                    {
                        SetDatas.Add(setData);
                    }
                }
                else
                {
                    HasSetData = false;
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

        public async Task DisplayByExerciceId(string exerciceId)
        {
            IsRunning = true;
            IsLoaded = false;

            try
            {
                if (string.IsNullOrEmpty(exerciceId))
                {
                    throw new Exception("L'id de l'exercice est vide");
                }

                Exercice? exercice = await _service.GetExerciceById(exerciceId);
                if (exercice == null)
                {
                    throw new Exception("Auncun exercice trouvé");
                }

                string userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("L'id de l'utilisateur est vide");
                }

                List<SetData>? setDatas = await _setDatasService.GetSetDataByExerciceAndUser(exercice.ExerciceId, userId);
                if (setDatas == null)
                {
                    throw new Exception("La serie est null");
                }

                if (setDatas.Count() != 0)
                {
                    SetDatas.Clear();

                    HasSetData = true;

                    foreach (SetData setData in setDatas)
                    {
                        SetDatas.Add(setData);
                    }
                }

                CurrentExercice = new Exercice
                {
                    ExerciceId = CurrentExercice.ExerciceId,
                    Nom = CurrentExercice.Nom,
                    UserId = CurrentExercice.UserId,
                    SetDatas = CurrentExercice.SetDatas
                };
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

        public void VerifyUserId()
        {
            if (string.IsNullOrEmpty(CurrentExercice.UserId) || CurrentExercice.UserId == "string")
            {
                HasPublic = false;
            }
            else
            {
                HasPublic = true;
            }
        }
    }
}
