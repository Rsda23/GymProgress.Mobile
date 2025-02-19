using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGymProgressViews(this IServiceCollection services)
        {
            services.AddTransient<SeancePage>();
            return services;
        }

        public static IServiceCollection AddGymProgressModels(this IServiceCollection services)
        {
            services.AddTransient<SeanceViewModel>();
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            return services;
        }
    }
}
