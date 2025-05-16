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
        private readonly ISeancesService _seancesService;
        private readonly SnackBarViewModel _snackBar;

        public AddSetDataPopupViewModel(ISetDatasService setDatasService, ISeancesService seancesService, string exerciceId, string seanceId, SnackBarViewModel snackBar)
        {
            _setDatasService = setDatasService;
            _seancesService = seancesService;
            ExerciceId = exerciceId;
            SeanceId = seanceId;
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
        private string seanceId;

        [ObservableProperty]
        private DateTime date = DateTime.Now;

        public Popup PopupInstance { get; set; } = new();



        [RelayCommand]
        private async Task Cancel()
        {
            await PopupInstance.CloseAsync();

            await Task.Delay(100);

            if (Shell.Current.CurrentPage is ExerciceDetailPage page && page.BindingContext is ExerciceDetailViewModel vm)
            {
                await vm.DisplayAddByName();
            }
        }

        [RelayCommand]
        private async Task Add()
        {
            IsRunning = true;

            try
            {
                string userId = Preferences.Get("UserId", string.Empty);
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("L'id de l'utilisateure est vide");
                }

                SetData setData = new SetData(ExerciceId, Repetition, Serie, Charge, Date, userId);

                await _setDatasService.PostSetData(setData);

                if (!string.IsNullOrEmpty(SeanceId))
                {
                    await _seancesService.UpdateDate(SeanceId);
                }

                await Cancel();

                _snackBar.Succefull("Série ajoutée !");
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
