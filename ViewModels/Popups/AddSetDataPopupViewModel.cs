using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;

namespace GymProgress.Mobile.ViewModels.Popups
{
    public partial class AddSetDataPopupViewModel : ViewModelBase
    {
        private readonly ISetDatasService _setDatasService;
        private readonly ExerciceDetailViewModel _exerciceDetailViewModel;

        public AddSetDataPopupViewModel(ISetDatasService setDatasService, string exerciceId, ExerciceDetailViewModel exerciceDetailViewModel)
        {
            _setDatasService = setDatasService;
            ExerciceId = exerciceId;
            _exerciceDetailViewModel = exerciceDetailViewModel;
        }

        [ObservableProperty]
        private string exerciceId = string.Empty;

        [ObservableProperty]
        private int serie;

        [ObservableProperty]
        private int repetition;

        [ObservableProperty]
        private float charge;

        [ObservableProperty]
        private DateTime date = DateTime.Now;

        public Popup PopupInstance { get; set; }



        [RelayCommand]
        private async void Cancel()
        {
            await PopupInstance.CloseAsync();

            if (Shell.Current.CurrentPage is ExerciceDetailPage page && page.BindingContext is ExerciceDetailViewModel vm)
            {
                await _exerciceDetailViewModel.DisplayAdd();
            }
        }

        [RelayCommand]
        private async void Add()
        {
            SetData setData = new SetData(ExerciceId, Serie, Repetition, Charge, Date);

            await _setDatasService.PostSetData(setData);

            Cancel();
        }
    }
}
