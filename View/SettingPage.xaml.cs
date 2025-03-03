using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile;

public partial class SettingPage : ContentPage
{
	private readonly SettingViewModel _viewModel;
	public SettingPage(SettingViewModel model)
	{
		InitializeComponent();
	}
}