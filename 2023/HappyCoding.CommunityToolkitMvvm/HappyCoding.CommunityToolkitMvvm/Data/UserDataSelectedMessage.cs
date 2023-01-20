namespace HappyCoding.CommunityToolkitMvvm.Data;

public class UserDataSelectedMessage
{
    public UserData? SelectedUserData { get; }

    public UserDataSelectedMessage(UserData? userData)
    {
        this.SelectedUserData = userData;
    }
}