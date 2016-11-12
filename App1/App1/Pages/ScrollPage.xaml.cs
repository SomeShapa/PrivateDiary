using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1.Pages
{
    public partial class ScrollPage : ContentPage
    {
        private ScrollPageViewModel _scrollPageViewModel;

        public ScrollPage()
        {
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, false);

            BindingContext = new ScrollPageViewModel();
            InitializeComponent();
            _scrollPageViewModel = (ScrollPageViewModel)BindingContext;
            this.ToolbarItems.Add(new ToolbarItem(String.Empty,Images.AddIcon,AddNote));
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
   RemoveLoginPage();
        }

        private static void RemoveLoginPage()
        {
            if (App.MainNavigation.Navigation.NavigationStack[0] is LoginPage)
            {
                App.MainNavigation.Navigation.RemovePage(App.MainNavigation.Navigation.NavigationStack[0]);
            }
        }

        private void AddNote()
        {
            _scrollPageViewModel.AddNote();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           
            _scrollPageViewModel.ItemSelected((UserNote)e.SelectedItem);
            ((ListView) sender).SelectedItem = null;
        }
    }
}
