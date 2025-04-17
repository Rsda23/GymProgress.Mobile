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

        public AddSetDataPopupViewModel(ISetDatasService setDatasService, string exerciceId)
        {
            _setDatasService = setDatasService;
            ExerciceId = exerciceId;
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
        }

        [RelayCommand]
        private async void Add()
        {
            SetData setData = new SetData(ExerciceId, Serie, Repetition, Charge, Date);

            _setDatasService.PostSetData(setData);

            MessagingCenter.Send(this, "DataChanged");
            Cancel();

            await Shell.Current.GoToAsync("../" + nameof(ExerciceDetailPage), true);
         
        }
    }
}
