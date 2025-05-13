using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace GymProgress.Mobile.ViewModels.SnackBar
{
    public partial class SnackBarViewModel : ViewModelBase
    {
        public async void Succefull(string message)
        {
            ISnackbar snackBar = Snackbar.Make(
                message: message,
                duration: TimeSpan.FromSeconds(2),
                visualOptions: new SnackbarOptions
                {
                    BackgroundColor = Colors.Purple,
                    TextColor = Colors.White,
                    CornerRadius = 10,
                    Font = Microsoft.Maui.Font.SystemFontOfSize(20),
                });

            await snackBar.Show();
        }
    }
}
