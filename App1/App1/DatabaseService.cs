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
    public class DatabaseService
    {
        private const string LocalUserCredKey = "LocalUserCred";
        private const string LocalUserNotesListKey = "LocalUserNotesList";

        
        private static IBlobCache Memory => BlobCache.LocalMachine;

        public static void Init()
        {
            BlobCache.ApplicationName = AppConfiguration.AppName;
        }

        public static async Task SetCurrentUserProfile(LoginInfo loginInfo)
          => await Memory.InsertObject(LocalUserCredKey, loginInfo);

        public static async Task<LoginInfo> GetCurrentUserProfile()
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

        public static async Task SetCurrentUserNotesList(ObservableCollection<UserNote> userNotes)
            => await Memory.InsertObject(LocalUserNotesListKey, userNotes);

        public static async Task<ObservableCollection<UserNote>> GetCurrentUserNotesList()
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
}
