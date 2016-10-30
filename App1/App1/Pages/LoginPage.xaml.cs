using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.ViewModels;
using Xamarin.Forms;

namespace App1.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext= new LoginPageViewModel();
        }

        private void Entry_OnCompleted(object sender, EventArgs e)
        {
           ((LoginPageViewModel)BindingContext).LogInCommand.Execute(null);
        }
    }
}
