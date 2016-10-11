using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class LoginPageViewModel
    {
         public ICommand LogInCommand { get; set; }

        public LoginPageViewModel()
        {
            LogInCommand=new Command(async () => await LogIn());
        }

        private async Task LogIn()
        {
            await App.MainNavigation.PushAsync(new ContentPage(),true);
        }
    }
}