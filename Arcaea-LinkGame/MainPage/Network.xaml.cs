using System.Windows;
using System.Windows.Controls;

namespace Arcaea_LinkGame.MainPage
{
    public partial class Network : Page
    {
        public Network()
        {
            InitializeComponent();
        }
        
        public void Disappear()
        {
            LogBtn.Visibility = Visibility.Hidden;
            LogBtn.IsHitTestVisible = false;
            RegBtn.Visibility = Visibility.Hidden;
            RegBtn.IsHitTestVisible = false;
            Tip.Visibility = Visibility.Hidden;
            Tip.IsHitTestVisible = false;
        }
        
        private void LogBtn_OnClick(object sender, RoutedEventArgs e)
        {
            FormTitle.Content = "Login";
            Disappear();
            User.Navigate(new LoginFrame(this));
        }

        private void RegBtn_OnClick(object sender, RoutedEventArgs e)
        {
            FormTitle.Content = "Register";
            Disappear();
            User.Navigate(new RegFrame());
        }

        public void showAccount()
        {
            FormTitle.Content = "Network";
            Disappear();
            User.Navigate(new Account(this));
        }

        public void backNet()
        {
            User.Navigate(null);
            LogBtn.Visibility = Visibility.Visible;
            LogBtn.IsHitTestVisible = true;
            RegBtn.Visibility = Visibility.Visible;
            RegBtn.IsHitTestVisible = true;
            Tip.Visibility = Visibility.Visible;
            Tip.IsHitTestVisible = true;
        }
    }
}