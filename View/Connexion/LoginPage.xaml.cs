using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile;

public partial class LoginPage : ContentPage
{
	private readonly LoginViewModel _loginViewModel;
    public LoginPage(LoginViewModel model)
	{
        InitializeComponent();
		_loginViewModel = model;
		BindingContext = _loginViewModel;
	}

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();

    //    EmailEntry.Text = string.Empty;
    //    PasswordEntry.Text = string.Empty;

    //    ErrorLogin.IsVisible = false;

    //}
}