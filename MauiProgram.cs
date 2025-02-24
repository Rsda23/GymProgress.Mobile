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
                //client.BaseAddress = new Uri("http://127.0.0.1:5000/");
                client.BaseAddress = new Uri("https://localhost:5001/");
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
