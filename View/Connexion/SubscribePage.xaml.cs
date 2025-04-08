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
}