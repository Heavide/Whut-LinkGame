using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Arcaea_LinkGame.User;

namespace Arcaea_LinkGame.MainPage
{
    public partial class MainPage : Page
    {
        private TopBar _topBar;

        private Network _network;

        private ScoreBoard _scoreBoard;

        private MainWindow _father;
        
        public MainPage(MainWindow fa)
        {
            InitializeComponent();
            _topBar = new TopBar();
            _network = new Network();
            _scoreBoard = new ScoreBoard();
            this.TopBar.Navigate(_topBar);
            this.Network.Navigate(_network);
            _topBar.Title.Content = "Arcaea-LinkGame";
            _father = fa;
        }
        
        private void network_click(object sender, RoutedEventArgs e)
        {
            if (UserAll.GetNow()._userName != "离线") _network.showAccount();
            else {
                _network = new Network();
                Network.Navigate(_network);
            }
            
            DoubleAnimation daX = new DoubleAnimation();
            daX.From = -550;
            daX.To = 0;
            daX.DecelerationRatio = 1;
            Duration duration = new Duration(TimeSpan.FromMilliseconds(500));
            daX.Duration = duration;
            this.NetworkAnim.BeginAnimation(TranslateTransform.XProperty, daX);
            this.NetExit.IsEnabled = true;
            this.NetExit.IsHitTestVisible = true;
        }

        private void network_exit(object sender, RoutedEventArgs e)
        {
            if (UserAll.GetNow()._userName != "离线") _network.showAccount();
            else {
                _network = new Network();
                Network.Navigate(_network);
            }
            
            DoubleAnimation daX = new DoubleAnimation();
            daX.From = 0;
            daX.To = -580;
            daX.DecelerationRatio = 1;
            Duration duration = new Duration(TimeSpan.FromMilliseconds(500));
            daX.Duration = duration;
            this.NetworkAnim.BeginAnimation(TranslateTransform.XProperty, daX);
            this.NetExit.IsEnabled = false;
            this.NetExit.IsHitTestVisible = false;
            _topBar.Change();
            this.TopBar.Refresh();
        }

        private void StartClicked(object sender, RoutedEventArgs e) {
            _father.ModePage();
        }

        private void Mode_OnClick(object sender, RoutedEventArgs e)
        {
            _father.OtherModePage();
        }

        private void Quit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Course_OnClick(object sender, RoutedEventArgs e)
        {
            _father.CoursePage();
        }

        private void score_exit(object sender, RoutedEventArgs e)
        {
            _scoreBoard = new ScoreBoard(); 
            Scoreboard.Navigate(_scoreBoard);
            
            DoubleAnimation daX = new DoubleAnimation();
            daX.From = 0;
            daX.To = 500;
            daX.DecelerationRatio = 1;
            Duration duration = new Duration(TimeSpan.FromMilliseconds(700));
            daX.Duration = duration;
            ScoreAnim.BeginAnimation(TranslateTransform.XProperty, daX);
            ScoreExit.IsEnabled = false;
            ScoreExit.IsHitTestVisible = false;
        }

        private void World_OnClick(object sender, RoutedEventArgs e)
        {
            UserAll.UserSave();
            _scoreBoard = new ScoreBoard(); 
            Scoreboard.Navigate(_scoreBoard);
            
            
            DoubleAnimation daX = new DoubleAnimation();
            daX.From = 500;
            daX.To = 0;
            daX.DecelerationRatio = 1;
            Duration duration = new Duration(TimeSpan.FromMilliseconds(700));
            daX.Duration = duration;
            ScoreAnim.BeginAnimation(TranslateTransform.XProperty, daX);
            ScoreExit.IsEnabled = true;
            ScoreExit.IsHitTestVisible = true;
        }
    }
}