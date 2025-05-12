using CommunityToolkit.Mvvm.ComponentModel;

namespace GymProgress.Mobile.ViewModels
{
    public partial class ViewModelBase : ObservableObject
    {
        [ObservableProperty]
        private bool isRunning;
    }
}
