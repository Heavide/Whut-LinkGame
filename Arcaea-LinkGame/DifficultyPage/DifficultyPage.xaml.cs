using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Arcaea_LinkGame.User;

namespace Arcaea_LinkGame.DifficultyPage;

public partial class DifficultyPage : Page
{

    private TopBar _topBar;

    private MainWindow _father;
    
    public DifficultyPage(MainWindow fa)
    {
        InitializeComponent();
        _topBar = new TopBar();
        TopBar.Navigate(_topBar);
        _father = fa;
        _topBar.Title.Content = "选择一个模式";
    }
    
    private void EasyDown(object sender, MouseButtonEventArgs e) { EasySelected.Visibility = Visibility.Visible; }

    private void EasyUp(object sender, MouseButtonEventArgs e) { EasySelected.Visibility = Visibility.Hidden; }

    private void NormalDown(object sender, MouseButtonEventArgs e) { NormalSelected.Visibility = Visibility.Visible; }
    
    private void NormalUp(object sender, MouseButtonEventArgs e) { NormalSelected.Visibility = Visibility.Hidden; }
    
    private void HardDown(object sender, MouseButtonEventArgs e) { HardSelected.Visibility = Visibility.Visible; }
    
    private void HardUp(object sender, MouseButtonEventArgs e) { HardSelected.Visibility = Visibility.Hidden; }

    private void BackClicked(object sender, RoutedEventArgs e) { _father.MainPage(); }

    private void Easy_OnClick(object sender, RoutedEventArgs e) { _father.GamePage(1); }
    
    private void Normal_OnClick(object sender, RoutedEventArgs e) { _father.GamePage(2); }
    
    private void Hard_OnClick(object sender, RoutedEventArgs e) { _father.GamePage(3); }
}