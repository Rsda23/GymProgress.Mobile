using GymProgress.Mobile.ViewModels;

namespace GymProgress.Mobile.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGymProgressViews(this IServiceCollection services)
        {
            services.AddTransient<LoginPage>();
            services.AddTransient<SeancePage>();
            services.AddTransient<SeanceDetailPage>();
            services.AddTransient<ExercicePage>();
            services.AddTransient<ExerciceDetailPage>();
            services.AddTransient<CreateSeancePage>();
            services.AddTransient<CreateExercicePage>();
            services.AddTransient<AddExercicePage>();
            services.AddTransient<SettingPage>();
            return services;
        }

        public static IServiceCollection AddGymProgressModels(this IServiceCollection services)
        {
            services.AddTransient<LoginViewModel>();
            services.AddTransient<SeanceViewModel>();
            services.AddTransient<SeanceDetailViewModel>();
            services.AddTransient<ExerciceViewModel>();
            services.AddTransient<ExerciceDetailViewModel>();
            services.AddTransient<CreateSeanceViewModel>();
            services.AddTransient<CreateExerciceViewModel>();
            services.AddTransient<AddExerciceViewModel>();
            services.AddTransient<SettingViewModel>();
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            return services;
        }
    }
}
