using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Arcaea_LinkGame.User;


namespace Arcaea_LinkGame.GamePage;

public partial class Result : Page
{
    
    private TopBar _topBar;

    private MainWindow _father;

    private int _mode;

    private int _end;

    private int _score;

    private BitmapImage _endimg;
    
    private BitmapImage _grade;

    private BitmapImage _icon;
    
    public Result(MainWindow father, int mode, int end, int score)
    {
        InitializeComponent();
        _father = father;
        _mode = mode;
        _end = end;
        _score = score;
        _topBar = new TopBar();
        TopBar.Navigate(_topBar);
        _topBar.Title.Content = "结果";
        
        GradeCheck();
        EndCheck();
        ShowLayout();
    }

    public void GradeCheck()
    {
        if (_score >= 9900000)
            _grade = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/grade/explus.png"));
        else if (_score >= 9800000)
            _grade = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/grade/ex.png"));
        else if (_score >= 9500000)
            _grade = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/grade/aa.png"));
        else if (_score >= 9200000)
            _grade = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/grade/a.png"));
        else if (_score >= 8900000)
            _grade = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/grade/b.png"));
        else if (_score >= 8600000)
            _grade = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/grade/c.png"));
        else
            _grade = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/grade/d.png"));
    }

    public void EndCheck()
    {
        if (_end == 0)
            _endimg = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/clear_fail.png"));
        else if(_end == 1)
            _endimg = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/clear_normal.png"));
        else if(_end == 2)
            _endimg = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/clear_pure.png"));
    }
    
    public void ShowLayout()
    {
        if (_mode == 1) ModeName.Content = "Easy";
        else if (_mode == 2) ModeName.Content = "Normal";
        else if (_mode == 3) ModeName.Content = "Hard";
        else if (_mode == 4) ModeName.Content = "Blind";
        else if (_mode == 5) ModeName.Content = "Mess";
        else if (_mode == 6) ModeName.Content = "Speed";
        else if (_mode == 7) ModeName.Content = "Course";

        Grade.Source = _grade;
        GradeName.Source = _endimg;
        Score.Content = _score;
        Icon.Source = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/icon/" + _mode + ".jpg"));
        
        if (UserAll.GetNow()._userName == "离线") return;
        HighScore.Content = UserAll.GetNow()._highScore[_mode];
        UserAll.GetNow()._highScore[_mode] = Math.Max(_score, UserAll.GetNow()._highScore[_mode]);
        UserAll.GetNow()._money += _score / 1000000;
        if (_mode == 7) UserAll.GetNow()._rating = Math.Max(_score * 1.0 / 1000000, UserAll.GetNow()._rating);
        UserAll.UserSave();
    }
    
    private void BackClicked(object sender, RoutedEventArgs e)
    {
        if(_mode <= 3) _father.ModePage();
        else if(_mode <= 6) _father.OtherModePage();
        else _father.MainPage();
    }
}