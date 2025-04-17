using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Core;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.View.Popups;
using GymProgress.Mobile.ViewModels.Popups;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace GymProgress.Mobile.ViewModels
{
    [QueryProperty(Constants.TargetProperties.ExerciceNom, Constants.QueryIdentifiers.ExerciceNom)]
    public partial class ExerciceDetailViewModel : ViewModelBase
    {
        private readonly IExercicesService _service;
        private readonly ISetDatasService _setDatasService;
        public ExerciceDetailViewModel(IExercicesService service, ISetDatasService setDatasService)
        {
            _service = service;
            _setDatasService = setDatasService;
        }

        [ObservableProperty]
        private Exercice currentExercice = new();

        [ObservableProperty]
        private string exerciceNom = string.Empty;

        [ObservableProperty]
        private string exerciceId = string.Empty;

        [ObservableProperty]
        private bool confirm = false;

        [ObservableProperty]
        private bool isEditing = false;

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
            IsEditing = false;
        }

        [RelayCommand]
        private async Task AddSetData(Exercice model)
        {
            var exercice = await _service.GetExerciceByName(ExerciceNom);
            var viewModel = new AddSetDataPopupViewModel(_setDatasService, exercice.ExerciceId, this);
            var popup = new AddSetDataPopup(viewModel);
            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
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

            if (exercice.SetDatas.Count() != 0)
            {
                SetDatas.Clear();

                EmptySetData = false;
                HasSetData = true;

                foreach (var setData in exercice.SetDatas)
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

        public async Task DisplayMessaging()
        {
            Exercice exercice = await _service.GetExerciceByName(ExerciceNom);

            if (exercice.SetDatas.Count() != 0)
            {
                SetDatas.Clear();

                EmptySetData = false;
                HasSetData = true;

                foreach (var setData in exercice.SetDatas)
                {
                    SetDatas.Add(setData);
                }
            }

            EmptySetData = false;
            HasSetData = true;
        }
    }
}
