using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App1.Annotations;

namespace App1.Pages
{
    public class UserNote:INotifyPropertyChanged
    {
        private DateTime _creationTime;
        private string _noteText ;
        private DateTime _lastEditingTime;

        public UserNote ()
        {
            _noteText = String.Empty;
         _creationTime=DateTime.Now;
    }

        public DateTime CreationTime
        {
            get { return _creationTime; }
            set
            {
                if (value.Equals(_creationTime)) return;
                _creationTime = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CreationTimePreview));
            }
        }

        public string CreationTimePreview => CreationTime.ToString($"dd MMMM{Environment.NewLine}yyyy");

        public DateTime LastEditingTime
        {
            get { return _lastEditingTime; }
            set
            {
                if (value.Equals(_lastEditingTime)) return;
                _lastEditingTime = value;
                OnPropertyChanged();
            }
        }

        public string NoteText
        {
            get { return _noteText; }
            set
            {
                if (value == _noteText) return;
                _noteText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NoteTextPreview));
            }
        }

        public string NoteTextPreview
        {
            get
            {
                if (NoteText.Length <= 25)
                {
                    return NoteText;
                }
                else
                {
                    return NoteText.Substring(0, 24) + "...";
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}