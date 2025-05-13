using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.ViewModels.SnackBar;

namespace GymProgress.Mobile.ViewModels.Popups
{
    public partial class AddSetDataPopupViewModel : ViewModelBase
    {
        private readonly ISetDatasService _setDatasService;
        private readonly SnackBarViewModel _snackBar;

        public AddSetDataPopupViewModel(ISetDatasService setDatasService, string exerciceId, SnackBarViewModel snackBar)
        {
            _setDatasService = setDatasService;
            ExerciceId = exerciceId;
            _snackBar = snackBar;
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

            await Task.Delay(100);

            if (Shell.Current.CurrentPage is ExerciceDetailPage page && page.BindingContext is ExerciceDetailViewModel vm)
            {
                await vm.DisplayAddByName();
            }
        }

        [RelayCommand]
        private async void Add()
        {
            string userId = Preferences.Get("UserId", string.Empty);

            SetData setData = new SetData(ExerciceId, Repetition, Serie, Charge, Date, userId);

            await _setDatasService.PostSetData(setData);

            Cancel();

            _snackBar.Succefull("Série ajoutée !");
        }
    }
}
