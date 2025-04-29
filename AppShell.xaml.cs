namespace GymProgress.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            foreach (var route in Routes.RouteTypeMap)
            {
                Routing.RegisterRoute(route.Key, route.Value);
            }

            var userId = Preferences.Get("UserId", string.Empty);

            if (string.IsNullOrEmpty(userId))
            {
                GoToAsync("//LoginPage");
            }
            else
            {
                GoToAsync("//SeancePage");
            }
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {

            base.OnNavigating(args);

            if (args.Source == ShellNavigationSource.ShellSectionChanged)
            {
                var navigation = Shell.Current.Navigation;
                var pages = navigation.NavigationStack;
                for (var i = pages.Count - 1; i >= 1; i--)
                {
                    navigation.RemovePage(pages[i]);
                }
            }
        }
    }
}
