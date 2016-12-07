using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Akavache;
using App1;
using App1.Pages;

public class MockDatabase : IDataBaseService
{
    public void Init(){}

    public LoginInfo LoginInfo { get; set; }
    public  ObservableCollection<UserNote> UserNotes{ get; set; }

    public Task SetCurrentUserProfile(LoginInfo loginInfo)
    {
        return Task.Run(() => LoginInfo = loginInfo);
    }

    public Task<LoginInfo> GetCurrentUserProfile()
    {
        return Task.FromResult(LoginInfo);
    }

    public Task SetCurrentUserNotesList(ObservableCollection<UserNote> userNotes)
    {
        return Task.Run(() => UserNotes = userNotes);
    }

    public Task<ObservableCollection<UserNote>> GetCurrentUserNotesList()
    {
        return Task.FromResult(UserNotes);
    }
}