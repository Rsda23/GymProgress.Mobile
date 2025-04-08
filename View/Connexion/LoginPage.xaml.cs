using GymProgress.Mobile.ViewModels;
using System.Diagnostics;

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
}