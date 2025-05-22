using System.ComponentModel;
using System.Windows;

namespace Arcaea_LinkGame.GameCore
{
    
    public class GameControl
    {
        //private GameLogic Logic;

        private GameLogicByGraph Logic;

        private int Score;

        public int BaseTime;
        
        public int mode;

        public int BonusTime;
        
        public int ClickedCnt;

        public int[] Choose;

        public int[] IsBlind;

        public Stack<int> Path;
        
        public GameControl(int mode)
        {
            this.mode = mode;
            Choose = new int[2];
            IsBlind = new int[10 * 10];
            ClickedCnt = 0;
            BonusTime = 5;
            
            //Logic = new GameLogic();
            Logic = new GameLogicByGraph();
            
            SetBaseTime();
        }
        
        public int GetId(int x, int y) { return Logic.GetId(x, y); }

        public void SetBaseTime()
        {
            if (mode == 1) BaseTime = 100;
            else if (mode == 2 || mode == 4 || mode == 5) BaseTime = 80;
            else if (mode == 3 || mode == 6 || mode == 7) BaseTime = 60;
        }
        
        public int GetBaseTime() { return BaseTime; }
        
        //-----------开始游戏-----------
        public bool StartGame(int row, int column, int cnt)
        {
            if (row * column % (cnt * 2) != 0) return false;
            Logic.BuildMap(row, column, cnt);
            return true;
        }

        public bool IsBlank(int id) {
            return Logic.BlankCheck(id);
        }
        
        public bool Link(int id1, int id2) {
            if (Logic.IsLink(id1, id2)) {
                Logic.BlankSet(id1);
                Logic.BlankSet(id2);
                Path = Logic.GetPath();
                return true;
            }
            return false;
        }
        
        public void BlindInit(int cnt)
        {
            IsBlind = new int[10 * 10];
            for (int i = 0; i <= cnt; i++) IsBlind[i] = 1;
            Random t = new Random();
            t.Shuffle(IsBlind);
        }

        public int[] Refresh() { return Logic.Refresh(); }
    }
}