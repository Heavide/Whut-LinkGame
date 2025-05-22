using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Arcaea_LinkGame.User;

public partial class TopBar : Page
{
    public TopBar() {
        InitializeComponent();
        Change();
    }

    private void TopDrag(object sender, MouseButtonEventArgs e) {
        Application.Current.MainWindow.DragMove();
    }

    public void SetName() {
        PlayerName.Content = UserAll.GetNow()._userName;
    }

    public void SetMoney() {
        Money.Content = UserAll.GetNow()._money;
    }

    public void SetRatting()
    {
        RattingBoard.Source = new BitmapImage(new Uri("pack://application:,,,../Resources/Main/topbar/rating.png"));
        RattingScore.Content = UserAll.GetNow()._rating;
    }
    
    public void Change() {
        SetName();
        SetMoney();
        if (UserAll.GetNow()._userName != "离线") SetRatting();
        else {
            RattingBoard.Source = null;
            RattingScore.Content = null;
        }
    }
}