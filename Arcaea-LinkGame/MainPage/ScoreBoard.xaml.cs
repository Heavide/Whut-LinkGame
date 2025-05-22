using System.Windows.Controls;
using Arcaea_LinkGame.User;

namespace Arcaea_LinkGame.MainPage;

public partial class ScoreBoard : Page
{
    private List<UserInfo> scoreboard;
    
    public ScoreBoard()
    {
        scoreboard = UserAll.GetScoreBoard();
        InitializeComponent();
        SetScoreBoard();
    }

    public void SetScoreBoard()
    {
        for (int i = 0; i < 5; i++)
        {
            if (scoreboard.Count <= i) break;
            var name = scoreboard[i]._userName;
            var rating = scoreboard[i]._rating;
            if (i == 0) First.Content = String.Format("{0,-10}", name) + String.Format("{0:F2}", rating);
            else if (i == 1) Second.Content = String.Format("{0,-10}", name) + String.Format("{0:F2}", rating);
            else if (i == 2) Third.Content = String.Format("{0,-10}", name) + String.Format("{0:F2}", rating);
            else if (i == 3) Fourth.Content = String.Format("{0,-10}", name) + String.Format("{0:F2}", rating);
            else if (i == 4) Fifth.Content = String.Format("{0,-10}", name) + String.Format("{0:F2}", rating);
        }
    }
}