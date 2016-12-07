using System.Collections.ObjectModel;
using System.Collections.Specialized;
using App1.Pages;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ScrollPageViewModel:BaseViewModel
    {
        public IDataBaseService DatabaseService { get; set; }
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

        public ScrollPageViewModel(IDataBaseService databaseService)
        {
            DatabaseService = databaseService;
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