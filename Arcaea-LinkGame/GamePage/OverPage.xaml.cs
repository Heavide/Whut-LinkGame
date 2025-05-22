using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Arcaea_LinkGame.GamePage;

public partial class OverPage : Page
{
    private int id;

    private GamePage _gamePage;

    private Color _startColor;
    
    private Color _endColor;

    private BitmapImage _top;

    private BitmapImage _bottom;

    private BitmapImage _center;

    private BitmapImage _text;
    
    public OverPage(GamePage gamePage, int id)
    {
        InitializeComponent();
        _gamePage = gamePage;
        this.id = id;
        IdSetting();
        SetBg();
        SetBorder();
        SetCenter();
        SetText();
        Anim();
    }

    public void IdSetting()
    {
        if (id <= 1) {
            _startColor = Color.FromRgb(90, 3, 82);
            _endColor = Color.FromRgb(90, 52, 117);
        }
        else {
            _startColor = Color.FromRgb(47, 3, 81);
            _endColor = Color.FromRgb(47, 72, 117);
        }
        
        if (id <= 1) {
            _top = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/end_stripe_top.png"));
            _bottom = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/end_stripe_bottom.png"));
        }
        else {
            _top = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/end_stripe_top_p.png"));
            _bottom = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/end_stripe_bottom_p.png"));
        }

        if (id == 0) {
            _center = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/end_mid_f.png"));
            _text = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/clear_fail.png"));
        }
        else if (id == 1) {
            _center = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/end_mid_c.png"));
            _text = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/clear_normal.png"));
        }
        else if (id == 2) {
            _center = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/end_mid_p.png"));
            _text = new BitmapImage(new Uri("pack://application:,,,../Resources/InGame/end/clear_pure.png"));
        }
    }
    
    public void SetBg()
    {
        var rec = new LinearGradientBrush();
        rec.StartPoint = new Point(0, 360);
        rec.EndPoint = new Point(960, 360);
        rec.GradientStops.Add(new GradientStop(_startColor, 0)); 
        rec.GradientStops.Add(new GradientStop(_endColor, 0));
        rec.Opacity = 0.7;
        Bg.Fill = rec;
    }

    public void SetBorder()
    {
        Top.Source = _top;
        Bottom.Source = _bottom;
    }

    public void SetCenter()
    {
        Center.Source = _center;
    }

    public void SetText()
    {
        Text.Source = _text;
    }

    public void Anim()
    {
        var storyboard = new Storyboard();

        var daV1 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)));
        daV1.DecelerationRatio = 1;
        Storyboard.SetTarget(daV1, Top);
        Storyboard.SetTargetProperty(daV1, new PropertyPath("Opacity"));
        storyboard.Children.Add(daV1);
        
        var daV2 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)));
        daV2.DecelerationRatio = 1;
        Storyboard.SetTarget(daV2, Bottom);
        Storyboard.SetTargetProperty(daV2, new PropertyPath("Opacity"));
        storyboard.Children.Add(daV2);
        
        var daV3 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)));
        daV3.DecelerationRatio = 1;
        Storyboard.SetTarget(daV3, Text);
        Storyboard.SetTargetProperty(daV3, new PropertyPath("Opacity"));
        storyboard.Children.Add(daV3);
        
        var daV4 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)));
        daV4.DecelerationRatio = 1;
        Storyboard.SetTarget(daV4, Center);
        Storyboard.SetTargetProperty(daV4, new PropertyPath("Opacity"));
        storyboard.Children.Add(daV4);
        
        var daC = new DoubleAnimation(0, 0.7, new Duration(TimeSpan.FromSeconds(1)));
        daC.DecelerationRatio = 1;
        Storyboard.SetTarget(daC, Bg);
        Storyboard.SetTargetProperty(daC, new PropertyPath("Opacity"));
        storyboard.Children.Add(daC);

        var daX = new ThicknessAnimation(
            new Thickness(-100, 0, 0, 0), new Thickness(0, 0, 0, 0), new Duration(TimeSpan.FromSeconds(1)));
        daX.DecelerationRatio = 1;
        Storyboard.SetTarget(daX, Top);
        Storyboard.SetTargetProperty(daX, new PropertyPath("Margin"));
        storyboard.Children.Add(daX);
        
        var daY = new ThicknessAnimation(
            new Thickness(0, 0, -100, 0), new Thickness(0, 0, 0, 0), new Duration(TimeSpan.FromSeconds(1)));
        daY.DecelerationRatio = 1;
        Storyboard.SetTarget(daY, Bottom);
        Storyboard.SetTargetProperty(daY, new PropertyPath("Margin"));
        storyboard.Children.Add(daY);

        var daS1 = new DoubleAnimation(1.3, 1.0, new Duration(TimeSpan.FromSeconds(1)));
        daS1.DecelerationRatio = 1;
        Center.RenderTransform = new ScaleTransform();
        Center.RenderTransformOrigin = new Point(0.5, 0.5);
        Storyboard.SetTarget(daS1, Center);
        Storyboard.SetTargetProperty(daS1, new PropertyPath("RenderTransform.ScaleX"));
        storyboard.Children.Add(daS1);
        
        var daS11 = new DoubleAnimation(1.3, 1.0, new Duration(TimeSpan.FromSeconds(1)));
        daS11.DecelerationRatio = 1;
        Center.RenderTransform = new ScaleTransform();
        Center.RenderTransformOrigin = new Point(0.5, 0.5);
        Storyboard.SetTarget(daS11, Center);
        Storyboard.SetTargetProperty(daS11, new PropertyPath("RenderTransform.ScaleY"));
        storyboard.Children.Add(daS11);
        
        var daS2 = new DoubleAnimation(1.3, 1.0, new Duration(TimeSpan.FromSeconds(1)));
        daS2.DecelerationRatio = 1;
        Text.RenderTransform = new ScaleTransform();
        Text.RenderTransformOrigin = new Point(0.5, 0.5);
        Storyboard.SetTarget(daS2, Text);
        Storyboard.SetTargetProperty(daS2, new PropertyPath("RenderTransform.ScaleX"));
        storyboard.Children.Add(daS2);
        
        var daS22 = new DoubleAnimation(1.3, 1.0, new Duration(TimeSpan.FromSeconds(1)));
        daS22.DecelerationRatio = 1;
        Text.RenderTransform = new ScaleTransform();
        Text.RenderTransformOrigin = new Point(0.5, 0.5);
        Storyboard.SetTarget(daS22, Text);
        Storyboard.SetTargetProperty(daS22, new PropertyPath("RenderTransform.ScaleY"));
        storyboard.Children.Add(daS22);

        var func = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(2)));
        Storyboard.SetTarget(func, Empty);
        Storyboard.SetTargetProperty(func, new PropertyPath("Height"));
        storyboard.Children.Add(func);

        storyboard.Completed += (sender, args) => { _gamePage.showResult(id); };
        
        storyboard.Begin();
    }
}