using GymProgress.Mobile.ViewModels.Connexion;

namespace GymProgress.Mobile.View.Connexion;

public partial class ForgotPage : ContentPage
{
    private readonly ForgotViewModel _forgotPage;
	public ForgotPage(ForgotViewModel model)
	{
		InitializeComponent();
        _forgotPage = model;
        BindingContext = _forgotPage;
	}
}