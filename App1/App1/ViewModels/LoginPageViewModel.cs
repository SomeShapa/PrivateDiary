using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Akavache;
using App1.Pages;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class LoginPageViewModel:BaseViewModel
    {
        private LoginInfo _userProfile;
        private string _buttonText;
        private ICommand _logInCommand;

        public ICommand LogInCommand
        {
            get { return _logInCommand; }
            set
            {
                if (Equals(value, _logInCommand)) return;
                _logInCommand = value;
                OnPropertyChanged();
            }
        }

        public string Password { get; set; }

        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                if (value == _buttonText) return;
                _buttonText = value;
                OnPropertyChanged();
            }
        }

        public LoginPageViewModel()
        {
            GetUserProfile();
           
        }

        private async Task CreateProfile()
        {
            await DatabaseService.SetCurrentUserProfile(new LoginInfo(null, Password));
            await App.MainNavigation.PushAsync(new ScrollPage(), true);
        }

        private async void GetUserProfile()
        {
            _userProfile = await DatabaseService.GetCurrentUserProfile();
            ButtonText = _userProfile != null ? "Log In" : "Save password";
            LogInCommand = _userProfile != null
               ? new Command(async () => await LogIn())
               : new Command(async () => await CreateProfile());
        }

        private async Task LogIn()
        {
             if (!String.IsNullOrWhiteSpace(Password) && Password.Length>=3 && Password.Length <= 20)
             {
                 if (CheckPassword(Password))
                 {
                     await App.MainNavigation.PushAsync(new ScrollPage(), true);
                 }
                 else
                 {
                    await App.MainNavigation.DisplayAlert("Error", "Wrong password!", "ok");
                }
             }
            else
             {
                await App.MainNavigation.DisplayAlert("Error", "Password must contain more than 3 and less then 20 symbols", "ok");
             }
        }

        private bool CheckPassword(string password) => password == _userProfile.Password;
    }
}