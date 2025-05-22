using System;
using System.Runtime.InteropServices.JavaScript;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace Arcaea_LinkGame.GameCore
{
    public class GameLogic
    {
        private int[] GameMap;
        private int[] IsBlank;
        private int rows;
        private int columns;
        private Stack<int> Path;
        public GameLogic()
        {
            Path = new Stack<int>();
            rows = columns = 10;
        }

        public int GetId(int x, int y) {
            return GameMap[GetPos(x, y)];
        }
        
        public void BuildMap(int row, int column, int cnt)
        {
            GameMap = new int[row * column];
            IsBlank = new int[row * column];
            rows = row;
            columns = column;
            var tmpCnt = 0;
            for (var i = 0; i < row * column; i++)
                GameMap[i] = (tmpCnt++) % cnt;
            Random t = new Random();
            t.Shuffle(GameMap);
        }

        public bool BlankCheck(int id) {
            if (IsBlank[id] == 1) return true;
            else return false;
        }

        public void BlankSet(int id)
        {
            IsBlank[id] = 1;
        }
        
        public int GetPos(int x, int y) {return x * columns + y;}
        
        public bool IsLink(int idx, int idy)
        {
            Path.Clear();
            Path.Push(idx);
            int xRow = idx / columns, xColumn = idx % columns;
            int yRow = idy / columns, yColumn = idy % columns;
            if (GameMap[idx] != GameMap[idy]) return false;
            if (xRow == yRow && LinkRow(xRow, xColumn, yColumn)) {Path.Push(idy); return true;}
            if (xColumn == yColumn && LinkColumn(xColumn, xRow, yRow)) {Path.Push(idy); return true;}
            if (LinkOneCorner(xRow, xColumn, yRow, yColumn)) {Path.Push(idy); return true;}
            if (LinkTwoCorner(xRow, xColumn, yRow, yColumn)) {Path.Push(idy); return true;}
            return false;
        }

        public bool LinkRow(int row, int xColumn, int yColumn)
        {
            for (int i = Math.Min(xColumn, yColumn) + 1; i < Math.Max(xColumn, yColumn); i++) 
                if (IsBlank[GetPos(row, i)] == 0) return false;
            return true;
        }

        public bool LinkColumn(int column, int xRow, int yRow)
        {
            for(int i = Math.Min(xRow, yRow) + 1; i < Math.Max(xRow, yRow); i ++)
                if (IsBlank[GetPos(i, column)] == 0) return false;
            return true;
        }

        public bool LinkOneCorner(int xRow, int xColumn, int yRow, int yColumn)
        {
            if (IsBlank[GetPos(xRow, yColumn)] == 1 && LinkRow(xRow, xColumn, yColumn) && LinkColumn(yColumn, xRow, yRow))
            {
                Path.Push(GetPos(xRow, yColumn));
                return true;
            }

            if (IsBlank[GetPos(yRow, xColumn)] == 1 && LinkColumn(xColumn, xRow, yRow) && LinkRow(yRow, xColumn, yColumn))
            {
                Path.Push(GetPos(yRow, xColumn));
                return true;
            }

            return false;
        }

        public bool LinkTwoCorner(int xRow, int xColumn, int yRow, int yColumn)
        {
            for (var i = 0; i < rows; i++) {
                if (IsBlank[GetPos(i, xColumn)] == 1 && IsBlank[GetPos(i, yColumn)] == 1 &&
                    LinkColumn(xColumn, xRow, i) && LinkRow(i, xColumn, yColumn) && LinkColumn(yColumn, i, yRow)) {
                    Path.Push(GetPos(i, xColumn));
                    Path.Push(GetPos(i, yColumn));
                    return true;
                }
            }

            for (var i = 0; i < columns; i++) {
                if (IsBlank[GetPos(xRow, i)] == 1 && IsBlank[GetPos(yRow, i)] == 1 &&
                    LinkRow(xRow, xColumn, i) && LinkColumn(i, xRow, yRow) && LinkRow(yRow, yColumn, i)) {
                    Path.Push(GetPos(xRow, i));
                    Path.Push(GetPos(yRow, i));
                    return true;
                } 
            }

            return false;
        }

        public Stack<int> GetPath()
        {
            return Path;
        }
    }
}