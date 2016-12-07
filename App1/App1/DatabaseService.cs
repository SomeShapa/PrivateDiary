using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Akavache;
using App1.Pages;

namespace App1
{
    public class DatabaseService: IDataBaseService
    {
        private const string LocalUserCredKey = "LocalUserCred";
        private const string LocalUserNotesListKey = "LocalUserNotesList";

        public DatabaseService()
        {
            Init();
        }
        private static IBlobCache Memory => BlobCache.LocalMachine;

        public  void Init()
        {
            BlobCache.ApplicationName = AppConfiguration.AppName;
        }

        public  async Task SetCurrentUserProfile(LoginInfo loginInfo)
          => await Memory.InsertObject(LocalUserCredKey, loginInfo);

        public  async Task<LoginInfo> GetCurrentUserProfile()
        {
            try
            {
                return await Memory.GetObject<LoginInfo>(LocalUserCredKey);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public  async Task SetCurrentUserNotesList(ObservableCollection<UserNote> userNotes)
            => await Memory.InsertObject(LocalUserNotesListKey, userNotes);

        public  async Task<ObservableCollection<UserNote>> GetCurrentUserNotesList()
        {
            try
            {
                return await Memory.GetObject<ObservableCollection<UserNote>>(LocalUserNotesListKey);
            }
            catch (Exception e)
            {
                return null;
            }
        }



    }

    public  interface IDataBaseService
    {
          void Init();

           Task SetCurrentUserProfile(LoginInfo loginInfo);

        Task<LoginInfo> GetCurrentUserProfile();


        Task SetCurrentUserNotesList(ObservableCollection<UserNote> userNotes);

       Task<ObservableCollection<UserNote>> GetCurrentUserNotesList();

    }
}
