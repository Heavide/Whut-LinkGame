using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using Newtonsoft.Json;

namespace Arcaea_LinkGame.User;

public static class UserAll
{
    private static Dictionary<string, UserInfo> _users = new Dictionary<string, UserInfo>();

    private static List<UserInfo> scoreboard = new List<UserInfo>();

    private static UserInfo _nowUser = new UserInfo("离线", "0");
    
    public static void UserLoad()
    {
        string filePath = "D:\\CS_Project\\Arcaea-LinkGame\\Arcaea-LinkGame\\User\\data\\users.json";
        string json = File.ReadAllText(filePath);
        _users = JsonConvert.DeserializeObject<Dictionary<string, UserInfo>>(json);
        LoadScoreBoard();
    }
    
    public static void UserSave()
    {
        string json = JsonConvert.SerializeObject(_users, Formatting.Indented);
        string filePath = "D:\\CS_Project\\Arcaea-LinkGame\\Arcaea-LinkGame\\User\\data\\users.json";
        File.WriteAllText(filePath, json);
        LoadScoreBoard();
    }

    public static bool HasUser(string name) { return _users.ContainsKey(name); }

    public static UserInfo LoginCheck(string name, string pswd)
    {
        UserInfo user;
        _users.TryGetValue(name, out user);
        if (user._userPswd != pswd) return null;
        SetNow(user);
        return user;
    }

    public static void SetNow(UserInfo x) { _nowUser = x; }

    public static void Quit()
    {
        _nowUser = new UserInfo("离线", "0");
    }

    public static UserInfo GetNow() { return _nowUser; }
    
    public static void RegUser(string name, string pswd)
    {
        var user = new UserInfo(name, pswd);
        _users.Add(name, user);
        UserSave();
    }

    public static void LoadScoreBoard()
    {
        scoreboard = new List<UserInfo>();
        foreach (var x in _users)
            scoreboard.Add(x.Value);
        
        scoreboard.Sort((x, y) => {
            return y._rating.CompareTo(x._rating);
        });
    }

    public static List<UserInfo> GetScoreBoard() { return scoreboard; }
}