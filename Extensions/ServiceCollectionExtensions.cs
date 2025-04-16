using GymProgress.Mobile.Services;
using GymProgress.Mobile.View.Connexion;
using GymProgress.Mobile.ViewModels;
using GymProgress.Mobile.ViewModels.Connexion;

namespace GymProgress.Mobile.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGymProgressViews(this IServiceCollection services)
        {
            services.AddTransient<LoginPage>();
            services.AddTransient<SubscribePage>();
            services.AddTransient<ForgotPage>();
            services.AddTransient<SeancePage>();
            services.AddTransient<SeanceDetailPage>();
            services.AddTransient<ExercicePage>();
            services.AddTransient<ExerciceDetailPage>();
            services.AddTransient<CreateSeancePage>();
            services.AddTransient<CreateExercicePage>();
            services.AddTransient<AddExercicePage>();
            services.AddTransient<SettingPage>();
            services.AddTransient<ExercicesService>();
            services.AddTransient<SeancesService>();
            services.AddTransient<UsersService>();
            services.AddTransient<SetDataService>();
            return services;
        }

        public static IServiceCollection AddGymProgressModels(this IServiceCollection services)
        {
            services.AddTransient<LoginViewModel>();
            services.AddTransient<SubscribeViewModel>();
            services.AddTransient<ForgotViewModel>();
            services.AddTransient<SeanceViewModel>();
            services.AddTransient<SeanceDetailViewModel>();
            services.AddTransient<ExerciceViewModel>();
            services.AddTransient<ExerciceDetailViewModel>();
            services.AddTransient<CreateSeanceViewModel>();
            services.AddTransient<CreateExerciceViewModel>();
            services.AddTransient<AddExerciceViewModel>();
            services.AddTransient<SettingViewModel>();
            services.AddTransient<ExercicesService>();
            services.AddTransient<SeancesService>();
            services.AddTransient<UsersService>();
            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            return services;
        }
    }
}
