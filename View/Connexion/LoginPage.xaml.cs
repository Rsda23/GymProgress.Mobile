using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.View.Connexion;
using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile;

public partial class LoginPage : ContentPage
{
	private readonly LoginViewModel _loginViewModel;
    private readonly IUsersService _usersService;
    private bool _isPasswordVisible = false;
    public LoginPage(LoginViewModel model, IUsersService usersService)
	{
        InitializeComponent();
		_loginViewModel = model;
		BindingContext = _loginViewModel;
        _usersService = usersService;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        EmailEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;

        ErrorLogin.IsVisible = false;

    }
    private async void Btn_Main(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Main");
    }
    private async void Go_Main()
    {
        await Shell.Current.GoToAsync("//Main");
    }
    private async void Btn_Subscribe(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Subscribe");
    }

    private async void Btn_Forgout(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ForgotPage());
    }
    private void ValidLogin(object sender, EventArgs e)
    {
        try
        {
            string inputEmail = EmailEntry.Text;
            string inputPassword = PasswordEntry.Text;
            string emailConnected = inputEmail;
            //string token = AssignToken();

            var collection = _mongoDbService.GetCollection<User>("users");
            var user = collection.Find(user => user.Email == inputEmail).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(inputEmail) || string.IsNullOrWhiteSpace(inputPassword))
            {
                throw new Exception("Tous les champs doivent être remplis.");
            }

            if (user == null)
            {
                ErrorLogin.Text = "Email ou mot de passe incorrect.";
                ErrorLogin.IsVisible = true;
                return;
            }

            bool password = PasswordHashing.ConfirmPassword(inputPassword, user.HashedPassword);

            if (!password)
            {
                ErrorLogin.Text = "Email ou mot de passe incorrect.";
                ErrorLogin.IsVisible = true;
                return;
            }

            Go_Main();
        }
        catch (Exception ex)
        {
            ErrorLogin.Text = ex.Message;
            ErrorLogin.IsVisible = true;
        }
    }
    private void PasswordVisibility(object sender, EventArgs e)
    {
        _isPasswordVisible = !_isPasswordVisible;
        PasswordEntry.IsPassword = !_isPasswordVisible;
        if (_isPasswordVisible)
        {
            EyeYes.IsVisible = false;
            EyeNo.IsVisible = true;
        }
        else
        {
            EyeYes.IsVisible = true;
            EyeNo.IsVisible = false;
        }
    }
}