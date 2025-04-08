using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymProgress.Domain.Models;
using GymProgress.Mobile.Services;
using System.Text.RegularExpressions;

namespace GymProgress.Mobile.ViewModels
{
    public partial class SubscribeViewModel : ViewModelBase
    {
        private readonly UsersService _usersService;
        public SubscribeViewModel(UsersService usersService)
        {
            _usersService = usersService;
        }

        [ObservableProperty]
        private string pseudo = string.Empty;
        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private bool layoutPseudo = true;
        [ObservableProperty]
        private bool layoutEmail = false;
        [ObservableProperty]
        private bool layoutPassword = false;

        [ObservableProperty]
        private bool errorPseudo = false;
        [ObservableProperty]
        private bool errorEmail = false;
        [ObservableProperty]
        private bool errorPassword = false;

        [ObservableProperty]
        private string errorPseudoText = string.Empty;
        [ObservableProperty]
        private string errorEmailText = string.Empty;
        [ObservableProperty]
        private string errorPasswordText = string.Empty;

        [ObservableProperty]
        private string firstPassword = string.Empty;
        [ObservableProperty]
        private string secondPassword = string.Empty;



        [RelayCommand]
        private async void GoToSeance()
        {
            await Shell.Current.GoToAsync($"/{Routes.SeancePage}");
        }

        [RelayCommand]
        private async void GoToLogin()
        {
            await Shell.Current.GoToAsync($"/{Routes.LoginPage}");
        }

        [RelayCommand]
        private async void EmailNext()
        {
            try
            {
                bool verify = ConfirmedPseudo(Pseudo);

                if (verify)
                {
                    ErrorPseudo = false;
                    LayoutPseudo = false;
                    LayoutEmail = true;
                    LayoutPassword = false;
                    await Task.Delay(500);
                }
            }
            catch (Exception ex)
            {
                ErrorPseudoText = ex.Message;
                ErrorPseudo = true;
            }
        }

        [RelayCommand]
        private async void PasswordNext()
        {
            try
            {
                bool verify = await ConfirmedEmail(Email);

                if (verify)
                {
                    ErrorEmail = false;
                    LayoutPseudo = false;
                    LayoutEmail = false;
                    LayoutPassword = true;
                    await Task.Delay(500);
                }
            }
            catch (Exception ex)
            {
                ErrorEmailText = ex.Message;
                ErrorEmail = true;
            }
        }

        [RelayCommand]
        public void ValidRegister()
        {
            try
            {
                bool verify = VerifyPassword(FirstPassword, SecondPassword);

                if (verify)
                {
                    string inputPseudo = Pseudo;
                    string inputEmail = Email;
                    string hashedPassword = PasswordHashing.HashPassword(FirstPassword);


                    var newUser = new User
                    {
                        Pseudo = inputPseudo,
                        Email = inputEmail,
                        HashedPassword = hashedPassword
                    };

                    _usersService.PostUser(newUser);

                    GoToSeance();
                }
            }
            catch (Exception ex)
            {
                ErrorPasswordText = ex.Message;
                ErrorPassword = true;
            }

        }

        public bool ConfirmedPseudo(string pseudo)
        {
            if (string.IsNullOrWhiteSpace(pseudo))
            {
                throw new Exception("Tous les champs doivent être remplis.");
            }

            if (pseudo.Count() >= 3 && pseudo.Count() <= 20)
            {
                return true;
            }
            else if (pseudo.Count() > 20)
            {
                throw new Exception("Le pseudo ne doit pas dépasser 20 caractères.");
            }
            else
            {
                throw new Exception("Le pseudo doit contenir au moins 3 caractères.");
            }
        }

        public async Task<bool> ConfirmedEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Tous les champs doivent être remplis.");
            }

            if (Regex.IsMatch(email, emailPattern))
            {
                bool verify = await VerifyEmail(email);
                if (verify)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Cette adresse email est déjà utilisée.");
                }
            }
            else
            {
                throw new Exception("L'adresse email n'est pas valide.");
            }
        }

        public async Task<bool> VerifyEmail(string email)
        {
            var user = await _usersService.GetUserByEmail(email);
            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VerifyPassword(string firstPassword, string secondPassword)
        {
            if (string.IsNullOrWhiteSpace(firstPassword) || string.IsNullOrWhiteSpace(secondPassword))
            {
                throw new Exception("Tous les champs doivent être remplis.");
            }

            if (firstPassword == secondPassword)
            {
                return ConfirmedPassword(firstPassword);
            }
            else
            {
                throw new Exception("Les mots de passe ne sont pas identique.");
            }
        }

        public bool ConfirmedPassword(string password)
        {
            if (password.Count() >= 8 && password.Count() <= 20)
            {
                return true;
            }
            else if (password.Count() > 20)
            {
                throw new Exception("Le mot de passe ne doit pas dépasser 20 caractères.");
            }
            else
            {
                throw new Exception("Le mot de passe doit contenir au moins 8 caractères.");
            }
        }

    }
}
