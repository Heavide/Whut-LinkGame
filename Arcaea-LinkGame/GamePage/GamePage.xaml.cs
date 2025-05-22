using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Arcaea_LinkGame.GameCore;

namespace Arcaea_LinkGame.GamePage
{
    public partial class GamePage : Page
    {
        public GameControl Control;
        private MainWindow _father;
        private Storyboard _timebar;
        private int _cnt;
        
        public LinkPage Link;
        
        public int TimeNum {
            get => (int)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register(nameof(TimeNum), typeof(int), typeof(GamePage), new PropertyMetadata(0));
        
        public int ScoreNum {
            get => (int)GetValue(ScoreProperty);
            set => SetValue(ScoreProperty, value);
        }

        public static readonly DependencyProperty ScoreProperty =
            DependencyProperty.Register(nameof(ScoreNum), typeof(int), typeof(GamePage), new PropertyMetadata(0));
        
        public GamePage(MainWindow fa, int mode) 
        {
            InitializeComponent();
            Control = new GameControl(mode);
            Link = new LinkPage(this, Control);
            Frame.Navigate(Link);
            _cnt = 0;
            _father = fa;
            BgSkin();

            Time.SetBinding(Label.ContentProperty, new Binding("TimeNum") { Source = this });
            Score.SetBinding(Label.ContentProperty, new Binding("ScoreNum") { Source = this });
            
            StartGame();
        }

        public void StartGame()
        {
            Control.StartGame(10, 10, 10);//负责调用Logic的BuildMap
            CountDown();//倒计时开始
            Link.BuildMap();//负责图片展示
            if(Control.mode == 4) Link.BlindMode();
            else if(Control.mode == 5) Link.MessMode();
            else if (Control.mode == 7) Link.CourseMode();
        }

        public void BgSkin()
        {
            InGame.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/bg/" + Control.mode + ".jpg")));
            Icon.Source =
                new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/icon/" + Control.mode + ".jpg"));
        }

        public void CountDown()
        {
            _timebar = new Storyboard();

            var daH = new DoubleAnimation(486, 0, new Duration(TimeSpan.FromSeconds(Control.GetBaseTime())));
            Storyboard.SetTarget(daH, TimeBar);
            Storyboard.SetTargetProperty(daH, new PropertyPath("(Image.Height)"));
            _timebar.Children.Add(daH);
            
            var daT = new Int32Animation(Control.GetBaseTime(), 0, new Duration(TimeSpan.FromSeconds(Control.GetBaseTime())));
            Storyboard.SetTarget(daT, Time);
            Storyboard.SetTargetProperty(daT, new PropertyPath("(Label.Content)"));
            _timebar.Children.Add(daT);
            
            var daTH = new ThicknessAnimation(new Thickness(0,0,0,0), new Thickness(0,470,0,0), new Duration(TimeSpan.FromSeconds(Control.GetBaseTime())));
            Storyboard.SetTarget(daTH, Time);
            Storyboard.SetTargetProperty(daTH, new PropertyPath("(Label.Padding)"));
            _timebar.Children.Add(daTH);
            if(Control.mode < 6) _timebar.Completed += (sender, args) => OverFrame.Navigate(new OverPage(this, 0));
            else _timebar.Completed += (sender, args) => OverFrame.Navigate(new OverPage(this, 1));
            _timebar.Begin();
            
        }

        public void AddTime()
        {
            if (Control.mode >= 6) return;
            try {
                _timebar.Seek(_timebar.GetCurrentTime().Subtract(TimeSpan.FromSeconds(Control.BonusTime)));
            }catch (Exception e) {
                _timebar.Seek(TimeSpan.FromSeconds(0));
            }
        }
        
        private void Pause_OnClick(object sender, RoutedEventArgs e)
        {
            PauseFrame.Navigate(new Pause(this));
            Link.Pause();
            _timebar.Pause();
        }

        public void AddScore()
        {
            int t = (Control.GetBaseTime() - _timebar.GetCurrentTime().Seconds) * 100 / Control.GetBaseTime();
            ScoreNum += t * 2000;
        }

        public void AddCnt(int x) {
            _cnt += x;
            if (_cnt >= 100)
            {
                OverFrame.Navigate(new OverPage(this, 2));
                _timebar.Stop();
            }
        }
        
        public void Resume()
        {
            PauseFrame.Navigate(null);
            Link.Resume();
            _timebar.Resume();
        }

        public void Quit()
        {
            if(Control.mode <= 3) _father.ModePage();
            else if(Control.mode <= 6) _father.OtherModePage();
            else _father.MainPage();
        }

        public void showResult(int end)
        {
            _father.ResultPage(Control.mode, end, ScoreNum);
        }
    }
}