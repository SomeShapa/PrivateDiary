﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading.Tasks;
using App1.ViewModels;
using Xamarin.Forms;

namespace App1.Pages
{
    

    public class ScrollPageViewModel:BaseViewModel
    {
        private  ObservableCollection<UserNote> _userNotes;

        public ObservableCollection<UserNote> UserNotes
        {
            get { return _userNotes; }
            set
            {
                if (Equals(value, _userNotes)) return;
                _userNotes = value;
                OnPropertyChanged();
                CollectionChanged?.BeginInvoke(_userNotes, null, null, null);
            }
        }

        public ScrollPageViewModel()
        {
            GetUserNotes();
        }

        private async void GetUserNotes()
        {
         UserNotes= await DatabaseService.GetCurrentUserNotesList() ?? new ObservableCollection<UserNote>();
        }

        public void ItemSelected(UserNote selectedItem)
        {
            if (selectedItem != null)
            {
                App.MainNavigation.PushAsync(new NoteEditPage(this,selectedItem));
            }
        }

        public void AddNote()
        {
            var userNote = new UserNote();
            App.MainNavigation.PushAsync(new NoteEditPage(this,userNote));
            UserNotes.Add(userNote);
        }

        public  void Save()
        {
            DatabaseService.SetCurrentUserNotesList(_userNotes);
        }

        public void DeleteNote(UserNote userNote)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UserNotes.Remove(userNote);
                
            }); 
             App.MainNavigation.PopAsync(true);
   
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}