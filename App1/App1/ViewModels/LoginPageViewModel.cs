using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Akavache;
using App1;
using App1.Annotations;
using App1.Pages;
using App1.ViewModels;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Mocks;
using Xamarin.Forms;
using Xunit;
using static System.Diagnostics.Debug;

namespace App1.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public IDataBaseService DatabaseService { get; set; }
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

        public LoginPageViewModel(IDataBaseService databaseService)
        {
            DatabaseService = databaseService;
            GetUserProfile();
        }


        private async Task CreateProfile()
        {

            if (!String.IsNullOrWhiteSpace(Password) && Password.Length >= 3 && Password.Length <= 20)
            {
                await DatabaseService.SetCurrentUserProfile(new LoginInfo(null, Password));
                await App.MainNavigation.PushAsync(new ScrollPage(DatabaseService), true);
            }
            else
            {
                await App.MainNavigation.DisplayAlert("Error",
                    "Password must contain more than 3 and less then 20 symbols", "ok");
            }
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
            if (!String.IsNullOrWhiteSpace(Password) && Password.Length >= 3 && Password.Length <= 20)
            {
                if (CheckPassword(Password))
                {
                    await App.MainNavigation.PushAsync(new ScrollPage(DatabaseService), true);
                }
                else
                {
                    await App.MainNavigation.DisplayAlert("Error", "Wrong password!", "ok");
                }
            }
            else
            {
                await
                    App.MainNavigation.DisplayAlert("Error",
                        "Password must contain more than 3 and less then 20 symbols", "ok");
            }
        }

        private bool CheckPassword(string password) => password == _userProfile.Password;
    }
}

public class LoginPageViewModelTests
{
    [Test]
    public void CheckAutentificationTrue()
    {
        App.MainNavigation = new NavigationPage(new Page());
        var loginpvm = new LoginPageViewModel(new MockDatabase
        {
            LoginInfo = new LoginInfo("", "qwe")
        })
        {
            Password = "qwe"
        };
        loginpvm.LogInCommand.Execute(null);
        NUnit.Framework.Assert.True(App.MainNavigation.CurrentPage.GetType() == typeof(ScrollPage));
    }

    [Test]
    public void CheckAutentificationFalse()
    {
        App.MainNavigation = new NavigationPage(new Page());
        var loginpvm = new LoginPageViewModel(new MockDatabase
        {
            LoginInfo = new LoginInfo("", "qwe1")
        })
        {
            Password = "qwe"
        };
        loginpvm.LogInCommand.Execute(null);
        NUnit.Framework.Assert.False(App.MainNavigation.CurrentPage.GetType() == typeof(ScrollPage));
    }
    [Test]
    public void CreateAccount()
    {
        App.MainNavigation = new NavigationPage(new Page());
        var loginpvm = new LoginPageViewModel(new MockDatabase())
        {
            Password = "qwer"
        };
        loginpvm.LogInCommand.Execute(null);
        var databasePassword = loginpvm.DatabaseService.GetCurrentUserProfile().Result.Password;
        NUnit.Framework.Assert.True(databasePassword=="qwer");
    }
}