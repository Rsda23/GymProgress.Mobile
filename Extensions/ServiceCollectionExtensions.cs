using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGymProgressViews(this IServiceCollection services)
        {
            services.AddTransient<MainPage>();
            return services;
        }

        public static IServiceCollection AddGymProgressModels(this IServiceCollection services)
        {
            services.AddTransient<MainViewModel>();
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            return services;
        }
    }
}
