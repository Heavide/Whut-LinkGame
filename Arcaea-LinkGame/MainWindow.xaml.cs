using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Arcaea_LinkGame.GamePage;
using Arcaea_LinkGame.MainPage;
using Arcaea_LinkGame.User;

namespace Arcaea_LinkGame;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        UserAll.UserLoad();
        Frame.Navigate(new MainPage.MainPage(this));
        
    }

    public void SwitchAnim(Object page)
    {
        var story = new Storyboard();

        var daXL = new ThicknessAnimationUsingKeyFrames();
        daXL.KeyFrames.Add(
            new LinearThicknessKeyFrame(new Thickness(0, 0, 0, 0), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500))));
        
        var daXR = new ThicknessAnimationUsingKeyFrames();
        daXR.KeyFrames.Add(
            new LinearThicknessKeyFrame(new Thickness(0, 0, 0, 0), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500))));
        
        daXL.Completed += (sender, args) => Frame.Navigate(page);
        
        Storyboard.SetTarget(daXL,Left);
        Storyboard.SetTarget(daXR,Right);
        Storyboard.SetTargetProperty(daXL, new PropertyPath("(Image.Margin)"));
        Storyboard.SetTargetProperty(daXR, new PropertyPath("(Image.Margin)"));
        
        story.Children.Add(daXL);
        story.Children.Add(daXR);
        
        story.Begin();
    }
    
    public void EndAnim()
    {
        var story = new Storyboard();

        var daXL = new ThicknessAnimationUsingKeyFrames();
        daXL.KeyFrames.Add(
            new LinearThicknessKeyFrame(new Thickness(0, 0, 0, 0), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500))));
        daXL.KeyFrames.Add(
            new LinearThicknessKeyFrame(new Thickness(0, 0, 0, 0), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(800))));
        daXL.KeyFrames.Add(
            new LinearThicknessKeyFrame(new Thickness(-1000, 0, 0, 0), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1400))));
        
        var daXR = new ThicknessAnimationUsingKeyFrames();
        daXR.KeyFrames.Add(
            new LinearThicknessKeyFrame(new Thickness(0, 0, 0, 0), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500))));
        daXR.KeyFrames.Add(
            new LinearThicknessKeyFrame(new Thickness(0, 0, 0, 0), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(800))));
        daXR.KeyFrames.Add(
            new LinearThicknessKeyFrame(new Thickness(0, 0, -300, 0), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1400))));
        
        Storyboard.SetTarget(daXL,Left);
        Storyboard.SetTarget(daXR,Right);
        Storyboard.SetTargetProperty(daXL, new PropertyPath("(Image.Margin)"));
        Storyboard.SetTargetProperty(daXR, new PropertyPath("(Image.Margin)"));
        
        story.Children.Add(daXL);
        story.Children.Add(daXR);
        
        story.Begin();
    }
    
    public void MainPage()
    {
        SwitchAnim(new MainPage.MainPage(this));
        EndAnim();
        while(Frame.CanGoBack) Frame.RemoveBackEntry();
    }
    public void ModePage()
    {
        SwitchAnim(new DifficultyPage.DifficultyPage(this));
        EndAnim();
        while(Frame.CanGoBack) Frame.RemoveBackEntry();
        
    }

    public void OtherModePage()
    {
        SwitchAnim(new DifficultyPage.OtherMode(this));
        EndAnim();
        while(Frame.CanGoBack) Frame.RemoveBackEntry();
    }
    public void GamePage(int mode)
    {
        SwitchAnim(new GamePage.GamePage(this, mode));
        EndAnim();
        while(Frame.CanGoBack) Frame.RemoveBackEntry();
    }

    public void ResultPage(int mode, int end, int score)
    {
        SwitchAnim(new Result(this, mode, end, score));
        EndAnim();
        while(Frame.CanGoBack) Frame.RemoveBackEntry();
    }

    public void CoursePage()
    {
        SwitchAnim(new GamePage.GamePage(this, 7));
        EndAnim();
        while(Frame.CanGoBack) Frame.RemoveBackEntry();
    }
    
}