using GymProgress.Mobile.ViewModels;
using System.Text.RegularExpressions;

namespace GymProgress.Mobile.View.Connexion;

public partial class SubscribePage : ContentPage
{
    private readonly SubscribeViewModel _subscribeViewModel;
    public SubscribePage(SubscribeViewModel model)
	{
		InitializeComponent();
        _subscribeViewModel = model;
        BindingContext = _subscribeViewModel;

	}

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();

    //    PseudoEntry.Text = string.Empty;
    //    EmailEntry.Text = string.Empty;
    //    FirstPasswordEntry.Text = string.Empty;
    //    ConfirmedPasswordEntry.Text = string.Empty;

    //    Pseudo.IsVisible = true;
    //    Email.IsVisible = false;
    //    Password.IsVisible = false;
    //}
}