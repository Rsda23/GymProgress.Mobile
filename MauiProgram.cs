using CommunityToolkit.Maui;
using epj.RouteGenerator;
using GymProgress.Mobile.Extensions;
using GymProgress.Mobile.Interfaces;
using GymProgress.Mobile.Services;
using Microsoft.Extensions.Logging;

namespace GymProgress.Mobile
{
    [AutoRoutes("Page")]
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.Services.AddGymProgressViews();
            builder.Services.AddGymProgressModels();
            builder.Services.AddInfrastructure();
            builder.Services.AddHttpClient<ISeancesService, SeancesService>(client =>
            {
                client.BaseAddress = new Uri("https://gymprogress-adezdfctcsdjhegr.francecentral-01.azurewebsites.net/");
            });
            builder.Services.AddHttpClient<IExercicesService, ExercicesService>(client =>
            {
                client.BaseAddress = new Uri("https://gymprogress-adezdfctcsdjhegr.francecentral-01.azurewebsites.net/");
            });

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
