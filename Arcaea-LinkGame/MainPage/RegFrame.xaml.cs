using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Arcaea_LinkGame.User;
namespace Arcaea_LinkGame.MainPage;

public partial class RegFrame : Page
{
    public RegFrame()
    {
        InitializeComponent();
    }

    private void RegBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var name = UserName.Text;
        var pswd = UserPswd.Password;
        var pswd2 = UserPswdAgain.Password;
        Tip.Foreground = Brushes.Red;
        if (!UserAll.HasUser(name)) {
            if (pswd == pswd2)
            {
                Tip.Foreground = Brushes.White;
                Tip.Content = "注册成功";
                UserAll.RegUser(name, pswd);
            }
            else Tip.Content = "前后密码不一致";
        }
        else Tip.Content = "用户名已存在";
        
    }
}