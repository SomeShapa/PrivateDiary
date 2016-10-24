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
            InitializeComponent();
            BindingContext = new ScrollPageViewModel();
            _scrollPageViewModel = (ScrollPageViewModel)BindingContext;
            this.ToolbarItems.Add(new ToolbarItem("Add",null,AddNote));
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

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var b = 5;
        }
    }
}
