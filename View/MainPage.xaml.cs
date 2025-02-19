using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _mainViewModel;
        public MainPage(MainViewModel model)
        {
            InitializeComponent();
            _mainViewModel = model;
            BindingContext = _mainViewModel;
        }
    }

}
