using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.Services;
using GymProgress.Mobile.ViewModels;
using System.Diagnostics;

namespace GymProgress.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
