namespace GymProgress.Mobile.View.Connexion;

public partial class ForgotPage : ContentPage
{
    private readonly ForgotPage _forgotPage;
	public ForgotPage(ForgotPage model)
	{
		InitializeComponent();
        _forgotPage = model;
        BindingContext = _forgotPage;
	}

    //private async void Btn_Mail(object sender, EventArgs e)
    //{
    //    Mail.IsVisible = false;
    //    Code.IsVisible = true;
    //    Password.IsVisible = false;
    //    await Task.Delay(500);
    //}
    //private async void Btn_Password(object sender, EventArgs e)
    //{
    //    Mail.IsVisible = false;
    //    Code.IsVisible = false;
    //    Password.IsVisible = true;
    //    await Task.Delay(500);
    //}

    //private async void Btn_Login(object sender, EventArgs e)
    //{
    //    await Shell.Current.GoToAsync("//Login");
    //}
}