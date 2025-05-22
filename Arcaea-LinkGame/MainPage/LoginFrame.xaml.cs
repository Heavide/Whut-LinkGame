using System.Windows;
using System.Windows.Controls;
using Arcaea_LinkGame.User;

namespace Arcaea_LinkGame.MainPage;

public partial class LoginFrame : Page
{
    private Network _network;
    
    public LoginFrame(Network network)
    {
        InitializeComponent();
        _network = network;
    }

    private void LogBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var name = UserName.Text;
        var pswd = UserPswd.Password;
        if (UserAll.HasUser(name))
        {
            if (UserAll.LoginCheck(name, pswd) != null)
            {
                Tip.Content = null;
                _network.showAccount();
            }
            else Tip.Content = "密码错误";
        }
        else Tip.Content = "用户名不存在";
    }
}