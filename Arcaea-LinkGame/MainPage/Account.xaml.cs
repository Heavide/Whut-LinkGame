using System.Windows;
using System.Windows.Controls;
using Arcaea_LinkGame.User;

namespace Arcaea_LinkGame.MainPage;

public partial class Account : Page
{

    private Network _network;
    public Account(Network network)
    {
        InitializeComponent();
        _network = network;
        show();
    }

    public void show()
    {
        Name.Content = UserAll.GetNow()._userName;
    }

    private void Quit_OnClick(object sender, RoutedEventArgs e) {
        UserAll.Quit();
        _network.backNet();
    }
}