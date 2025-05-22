using System.Windows;
using System.Windows.Controls;

namespace Arcaea_LinkGame.GamePage;

public partial class Pause : Page
{

    private GamePage InGame;
    public Pause(GamePage gamePage)
    {
        InitializeComponent();
        InGame = gamePage;
    }

    private void Resume_OnClick(object sender, RoutedEventArgs e)
    {
        InGame.Resume();
    }

    private void Quit_OnClick(object sender, RoutedEventArgs e)
    {
        InGame.Quit();
    }
}