using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Arcaea_LinkGame.User;

namespace Arcaea_LinkGame.DifficultyPage;

public partial class OtherMode : Page
{
    
    private TopBar _topBar;

    private MainWindow _father;
    
    public OtherMode(MainWindow fa)
    {
        InitializeComponent();
        _topBar = new TopBar();
        TopBar.Navigate(_topBar);
        _father = fa;
        _topBar.Title.Content = "选择一个模式";
    }
    
    private void BackClicked(object sender, RoutedEventArgs e) { _father.MainPage(); }
    
    private void Blind_OnClick(object sender, RoutedEventArgs e) { _father.GamePage(4); }
    
    private void Mess_OnClick(object sender, RoutedEventArgs e) { _father.GamePage(5); }
    
    private void Speed_OnClick(object sender, RoutedEventArgs e) { _father.GamePage(6); }
    
    private void BlindDown(object sender, MouseButtonEventArgs e) { BlindSelected.Visibility = Visibility.Visible; }

    private void BlindUp(object sender, MouseButtonEventArgs e) { BlindSelected.Visibility = Visibility.Hidden; }

    private void MessDown(object sender, MouseButtonEventArgs e) { MessSelected.Visibility = Visibility.Visible; }

    private void MessUp(object sender, MouseButtonEventArgs e) { MessSelected.Visibility = Visibility.Hidden; }
    
    private void SpeedDown(object sender, MouseButtonEventArgs e) { SpeedSelected.Visibility = Visibility.Visible; }

    private void SpeedUp(object sender, MouseButtonEventArgs e) { SpeedSelected.Visibility = Visibility.Hidden; }
    

    
}