using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile;

public partial class SettingPage : ContentPage
{
	private readonly SettingViewModel _settingViewModel;
	public SettingPage(SettingViewModel model)
	{
		InitializeComponent();
		_settingViewModel = model;
		BindingContext = _settingViewModel;
	}
}