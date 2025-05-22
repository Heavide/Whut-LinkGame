using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Arcaea_LinkGame.GameCore;

namespace Arcaea_LinkGame.GamePage;

public partial class LinkPage : Page
{
    private GamePage _gamePage;
    
    private GameControl Control;

    private Polyline _polyline;

    private int[] isClicked;

    private Storyboard _storyboard;

    public LinkPage(GamePage gamePage, GameControl gameControl)
    {
        InitializeComponent();
        _gamePage = gamePage;
        _storyboard = new Storyboard();
        Control = gameControl;
        isClicked = new int[10 * 10];
    }
    public void BuildMap()
    {
        Canvas.Children.Clear();
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                Image img = new Image();
                img.Height = 45;
                img.Width = 45;
                img.Tag = i * 10 + j;
                Canvas.RegisterName("img" + (i * 10 + j).ToString(), img);
                img.Source = new BitmapImage(
                    new Uri("pack://application:,,,../Resources/item/" + Control.mode + "/" + Control.GetId(i, j) + ".jpg"));
                img.MouseLeftButtonDown += ImageClick;
                Canvas.SetTop(img, i * 55);
                Canvas.SetLeft(img, j * 55);
                Canvas.Children.Add(img);
            }
        }
    }

    public void ImageClick(object sender, MouseEventArgs e)
    {
        Image img = (Image)sender;
        if (Control.IsBlank((int)img.Tag)) return;
        if (isClicked[(int)img.Tag] == 0) {
            var shadow = new DropShadowEffect();
            shadow.BlurRadius = 25;
            img.Effect = shadow;
            isClicked[(int)img.Tag] = 1;
        }
        else {
            img.Effect = null;
            isClicked[(int)img.Tag] = 0;
            Control.Choose[Control.ClickedCnt] = 0;
            Control.ClickedCnt = (Control.ClickedCnt + 1) % 2;
            return;
        }

        Control.ClickedCnt = (Control.ClickedCnt + 1) % 2;
        Control.Choose[Control.ClickedCnt] = (int)img.Tag;
        if (Control.ClickedCnt == 0)
        {
            Image img1 = (Image)this.FindName("img" + Control.Choose[0]);
            Image img2 = (Image)this.FindName("img" + Control.Choose[1]);
            img1.Effect = null;
            img2.Effect = null;
            isClicked[(int)img1.Tag] = 0;
            isClicked[(int)img2.Tag] = 0;
            if (Control.Link((int)img1.Tag, (int)img2.Tag)) {
                ImageDisappear(img1);
                ImageDisappear(img2);
                _gamePage.AddTime();
                _gamePage.AddScore();
                DrawLine();
                _gamePage.AddCnt(2);
            } else {
                ImageWrong(img1);
                ImageWrong(img2);
            }
        }
    }

    public void DrawLine()
    {
        Stack<int> Path = Control.Path;
        _polyline = new Polyline();
        while (Path.Count != 0)
        {
            int id = Path.Pop();
            var img = (Image)FindName("img" + id);
            var point = img.TranslatePoint(new Point(), Canvas);
            point.X += 22.5;
            point.Y += 22.5;
            _polyline.Points.Add(point);
        }
        
        _polyline.StrokeThickness = 3;
        _polyline.Stroke = Brushes.Black;
        Canvas.Children.Add(_polyline);
        LineAnim(_polyline);
    }
    
    
    public void ImageDisappear(Image img)
    {
        var point = img.TranslatePoint(new Point(), Canvas);
        var pure = new Image();
        pure.Source = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/hit_pure.png"));
        Canvas.SetTop(pure, point.Y);
        Canvas.SetLeft(pure, point.X);
        Canvas.Children.Add(pure);
        ImgAnim(img);
        TagAnim(pure);
    }

    public void ImageWrong(Image img)
    {
        var point = img.TranslatePoint(new Point(), Canvas);
        var lost = new Image();
        lost.Source = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/hit_lost.png"));
        Canvas.SetTop(lost, point.Y);
        Canvas.SetLeft(lost, point.X);
        Canvas.Children.Add(lost);
        TagAnim(lost);
    }
    
    public void ImgAnim(Image img)
    {
        var daV = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(1000));
        daV.DecelerationRatio = 1;
        img.BeginAnimation(OpacityProperty, daV);
    }

    public void LineAnim(Polyline line)
    {
        var daV = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(1000));
        daV.DecelerationRatio = 1;
        line.BeginAnimation(OpacityProperty, daV);
    }
    
    public void TagAnim(Image img)
    {
        img.Height = 44;
        img.Width = 47;
        img.IsHitTestVisible = false;
        
        var daY = new DoubleAnimation();
        daY.From = (img.TranslatePoint(new Point(), Canvas).Y + 100) * 0.001;
        daY.By = -10;
        daY.DecelerationRatio = 1;
        daY.Duration = new Duration(TimeSpan.FromMilliseconds(500));
        var t = new TranslateTransform();
        img.RenderTransform = t;
        t.BeginAnimation(TranslateTransform.YProperty, daY);
        
        var daV = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(1000));
        daV.DecelerationRatio = 1;
        img.BeginAnimation(OpacityProperty, daV);
    }

    public void Pause() { _storyboard.Pause(); }

    public void Resume() { _storyboard.Resume(); }
    
    //BlindMode
    public void BlindMode()
    {
        _storyboard = new Storyboard();
        var func = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(12)));
        Storyboard.SetTarget(func, Empty);
        Storyboard.SetTargetProperty(func, new PropertyPath("Height"));
        _storyboard.Children.Add(func);

        _storyboard.Completed += (sender, args) => {
            Control.BlindInit(30);
            BlindMapSet();
            _storyboard.Begin();
        };
        _storyboard.Begin();
    }

    public void BlindMapSet()
    {
        for (int i = 0; i < 10 * 10; i++) {
            Image img = (Image)FindName("img" + i);
            if(Control.IsBlind[i] == 0)
                img.Source = new BitmapImage(new Uri("pack://application:,,,../Resources/item/" + Control.mode + "/" + Control.GetId(i / 10, i % 10) + ".jpg"));
            else 
                img.Source = new BitmapImage(new Uri("pack://application:,,,../Resources/item/" + Control.mode + "/-1.jpg"));
        }
    }
    
    //Mess

    public void MessMode()
    {
        _storyboard = new Storyboard();
        var func = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(12)));
        Storyboard.SetTarget(func, Empty);
        Storyboard.SetTargetProperty(func, new PropertyPath("Height"));
        _storyboard.Children.Add(func);
    
        _storyboard.Completed += (sender, args) => {
            MessMapSet();
            _storyboard.Begin();
        };
        _storyboard.Begin();
    }

    public void MessMapSet()
    {
        int[] refresh = Control.Refresh();
        for (int i = 0; i < 10 * 10; i++)
        {
            int j = refresh[i];
            if (i == j) continue;
            var img1 = (Image)FindName("img" + i);
            var img2 = (Image)FindName("img" + j);
            (img1.Tag, img2.Tag) = (img2.Tag, img1.Tag);
            Canvas.UnregisterName("img" + i);
            Canvas.UnregisterName("img" + j);
            Canvas.RegisterName("img" + j, img1);
            Canvas.RegisterName("img" + i, img2);
            
            Canvas.SetTop(img1, j / 10 * 55);
            Canvas.SetLeft(img1, j % 10 * 55);
            
            Canvas.SetTop(img2, i / 10 * 55);
            Canvas.SetLeft(img2, i % 10 * 55);
        }
    }
    
    //Course

    public void CourseMode()
    {
        int x = 0;
        _storyboard = new Storyboard();
        var func = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(10)));
        Storyboard.SetTarget(func, Empty);
        Storyboard.SetTargetProperty(func, new PropertyPath("Height"));
        _storyboard.Children.Add(func);
        
        _storyboard.Completed += (sender, args) => {
            Control.BlindInit(15);
            if(x % 2 == 0) BlindMapSet();
            else MessMapSet();
            x++;
            _storyboard.Begin();
        };
        _storyboard.Begin();
    }
}