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

	private void ThemeMode(object sender, ToggledEventArgs e)
	{
		bool isThemeMode = e.Value;

		if (Application.Current != null)
		{
			Application.Current.UserAppTheme = isThemeMode ? AppTheme.Dark : AppTheme.Light;
		}
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
		await _settingViewModel.LoadUser();
    }
}