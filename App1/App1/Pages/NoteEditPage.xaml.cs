using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1.Pages
{
    public partial class NoteEditPage : ContentPage
    {
        private readonly ScrollPageViewModel _scrollPageVm;

        public NoteEditPage(ScrollPageViewModel scrollPageVM, UserNote userNote)
        {
            _scrollPageVm = scrollPageVM;
            BindingContext = userNote;
            InitializeComponent();
        }

      

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _scrollPageVm.Save();
        }

       
    }
}
