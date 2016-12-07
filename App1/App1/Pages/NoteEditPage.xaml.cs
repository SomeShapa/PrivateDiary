using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.ViewModels;
using Xamarin.Forms;

namespace App1.Pages
{
    public partial class NoteEditPage : ContentPage
    {
        private readonly ScrollPageViewModel _scrollPageVm;

        public NoteEditPage(ScrollPageViewModel scrollPageVM, UserNote userNote)
        {
            _scrollPageVm = scrollPageVM;
           

            this.ToolbarItems.Add(new ToolbarItem(String.Empty, Images.DeleteIcon, DeleteNote));
            BindingContext = userNote;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (TextEditor.Text == String.Empty)
            {
                TextEditor.Focus();
            }
        }

        private void DeleteNote()
        {
            _scrollPageVm.DeleteNote(BindingContext as UserNote);
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _scrollPageVm.Save();
        }
    }
}
