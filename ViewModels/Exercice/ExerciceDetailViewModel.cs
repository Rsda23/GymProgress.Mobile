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
using System.Diagnostics;
using System.Text.Json;

namespace GymProgress.Mobile.ViewModels
{
    [QueryProperty(Constants.TargetProperties.ExerciceNom, Constants.QueryIdentifiers.ExerciceNom)]
    public partial class ExerciceDetailViewModel : ViewModelBase
    {
        private readonly IExercicesService _service;
        private readonly ISetDatasService _setDatasService;
        private readonly SnackBarViewModel _snackBar;
        public ExerciceDetailViewModel(IExercicesService service, ISetDatasService setDatasService, SnackBarViewModel snackBar)
        {
            _service = service;
            _setDatasService = setDatasService;
            _snackBar = snackBar;
        }

        [ObservableProperty]
        private Exercice currentExercice = new();

        [ObservableProperty]
        private string exerciceNom = string.Empty;

        [ObservableProperty]
        private string exerciceId = string.Empty;

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
        private bool emptySetData;

        [ObservableProperty]
        private ObservableCollection<SetData> setDatas = new();


        [RelayCommand]
        private async Task Delete()
        {
            Confirm = await Shell.Current.DisplayAlert(
                "Confirmation",
                "Voulez-vous vraiment supprimer cet exercice ?",
                "Oui", "Non");

            if (Confirm)
            {
                await _service.Delete(CurrentExercice.ExerciceId);
                await Shell.Current.GoToAsync("..");

                _snackBar.Succefull("Suppression effectuée !");
            }
        }

        [RelayCommand]
        private async Task Editing()
        {
            IsEditing = !IsEditing;
        }

        [RelayCommand]
        private async Task SaveEdit()
        {
            foreach (var setData in SetDatas)
            {
                await _setDatasService.ReplaceSetData(setData);
            }

            await _service.UpdateExercice(CurrentExercice.ExerciceId, CurrentExercice.Nom);

            IsEditing = false;

            await DisplayByExerciceId(CurrentExercice.ExerciceId);

        }

        [RelayCommand]
        private async Task AddSetData(Exercice model)
        {
            var exercice = await _service.GetExerciceByName(ExerciceNom);
            var viewModel = new AddSetDataPopupViewModel(_setDatasService, exercice.ExerciceId, this, _snackBar);
            var popup = new AddSetDataPopup(viewModel);
            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        [RelayCommand]
        private async Task DeleteSetData(SetData setData)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmation", "Etes vous sure de vouloir suprrimer cette serie ?", "Oui", "Non");

            if (confirm)
            {
                await _setDatasService.Delete(setData.SetDataId);
                await DisplaySetData();

                _snackBar.Succefull("Suppression effectuée !");
            }
        }



        async partial void OnExerciceNomChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                CurrentExercice = await _service.GetExerciceByName(value);
            }
        }

        public async Task DisplaySetData()
        {
            Exercice exercice = await _service.GetExerciceByName(ExerciceNom);
            string userId = Preferences.Get("UserId", string.Empty);
            List<SetData> setDatas = await _setDatasService.GetSetDataByExerciceAndUser(exercice.ExerciceId, userId);

            if (setDatas.Count() != 0)
            {
                SetDatas.Clear();

                EmptySetData = false;
                HasSetData = true;

                foreach (var setData in setDatas)
                {
                    SetDatas.Add(setData);
                }
            }
            else
            {
                HasSetData = false;
                EmptySetData = true;
            }
        }

        public async Task DisplayAddByName()
        {
            Exercice exercice = await _service.GetExerciceByName(ExerciceNom);
            string userId = Preferences.Get("UserId", string.Empty);
            List<SetData> setDatas = await _setDatasService.GetSetDataByExerciceAndUser(exercice.ExerciceId, userId);

            if (setDatas.Count() != 0)
            {
                SetDatas.Clear();

                EmptySetData = false;
                HasSetData = true;

                foreach (var setData in setDatas)
                {
                    SetDatas.Add(setData);
                }
            }

            EmptySetData = false;
            HasSetData = true;
        }

        public async Task DisplayByExerciceId(string exerciceId)
        {
            Exercice exercice = await _service.GetExerciceById(exerciceId);
            string userId = Preferences.Get("UserId", string.Empty);
            List<SetData> setDatas = await _setDatasService.GetSetDataByExerciceAndUser(exercice.ExerciceId, userId);

            if (setDatas.Count() != 0)
            {
                SetDatas.Clear();

                EmptySetData = false;
                HasSetData = true;

                foreach (var setData in setDatas)
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

            EmptySetData = false;
            HasSetData = true;
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
