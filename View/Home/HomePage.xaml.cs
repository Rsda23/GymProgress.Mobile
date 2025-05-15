using GymProgress.Mobile.ViewModels.Home;

namespace GymProgress.Mobile;

public partial class HomePage : ContentPage
{
	private readonly HomeViewModel _homeViewModel;
	public HomePage(HomeViewModel homeViewModel)
	{
		InitializeComponent();
		_homeViewModel = homeViewModel;
		BindingContext = _homeViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = CallSeance();
    }

    private async Task CallSeance()
    {
        await _homeViewModel.DisplayLastSeances();
    }
}