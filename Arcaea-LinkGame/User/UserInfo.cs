namespace Arcaea_LinkGame.User;

using Newtonsoft.Json;
public class UserInfo
{
    public string _userName;

    public string _userPswd;

    public int[] _highScore;

    public int _money;

    public double _rating;

    public UserInfo(string name, string password)
    {
        _userName = name;
        _userPswd = password;
        _highScore = new int[8];
        _money = 0;
    }
}