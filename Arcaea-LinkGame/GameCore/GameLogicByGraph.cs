namespace Arcaea_LinkGame.GameCore
{
    public class GameLogicByGraph
    {
        private int[] GameMap;
        private int[] IsBlank;
        private Tuple<int, int>[,] pre;
        private int[] refresh;
        private LinkedList<int>[] Graph;

        private int corners;
        private int rows;
        private int columns;
        
        private Stack<int> Path;
        
        public GameLogicByGraph()
        {
            Path = new Stack<int>();
            corners = 2;
            rows = columns = 10;
        }

        public int GetId(int x, int y) { return GameMap[GetPos(x, y)]; }
        
        public void BuildMap(int row, int column, int cnt)
        {
            GameMap = new int[row * column];
            IsBlank = new int[row * column];
            Graph = new LinkedList<int>[row * column];
            rows = row;
            columns = column;
            var tmpCnt = 0;
            for (var i = 0; i < row * column; i++)
                GameMap[i] = (tmpCnt++) % cnt;
            Random t = new Random();
            t.Shuffle(GameMap);
            
            BuildGraph();
        }

        public void BuildGraph()
        {
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { 1, 0, -1, 0 };
            for (int i = 0; i < rows * columns; i++) {
                int id = GameMap[i];
                Graph[i] = new LinkedList<int>();
                int x = i / 10, y = i % 10;
                for (int j = 0; j < 4; j++) {
                    int xx = x + dx[j], yy = y + dy[j];
                    if (xx < 0 || xx >= rows || yy < 0 || yy >= columns) continue;
                    int next = GetPos(xx, yy);
                    if (GameMap[next] == id || BlankCheck(i) || BlankCheck(next)) Graph[i].AddLast(next);
                }
            }
        }
        
        public bool BlankCheck(int id) {
            if (IsBlank[id] == 1) return true;
            else return false;
        }

        public void BlankSet(int id) { IsBlank[id] = 1; }
        
        public int GetPos(int x, int y) {return x * columns + y;}

        
        //上:0, 左:1, 下:2, 右:3
        public int GetDirect(int id1, int id2)
        {
            int x1 = id1 / columns, y1 = id1 % columns;
            int x2 = id2 / columns, y2 = id2 % columns;
            if (y1 == y2) {
                if (x2 < x1) return 0;
                return 2;
            }
            else {
                if (y2 < y1) return 1; 
                return 3;
            }
        }
        
        public bool IsLink(int idx, int idy)
        {
            if (GameMap[idx] != GameMap[idy]) return false;
            Path.Clear();
            if (bfs(idx, idy)) {
                LinkEmpty(idx);
                LinkEmpty(idy);
                return true;
            }

            return false;
        }

        public bool bfs(int u, int v)
        {
            pre = new Tuple<int, int>[rows * columns, 4];
            Tuple<int, int> res = new Tuple<int, int>(-1, 0);
            
            int[,] min_corner = new int[rows * columns,4];
            for (int i = 0; i < rows * columns; i++)
                for(int j = 0; j < 4; j ++)
                    min_corner[i, j] = corners + 1;
            min_corner[u, 0] = 0;
            min_corner[u, 1] = 0;
            min_corner[u, 2] = 0;
            min_corner[u, 3] = 0;
            
            Queue<Tuple<int, int, int>> q = new Queue<Tuple<int, int, int>>();
            q.Enqueue(new Tuple<int, int, int>(u,-1,-1));
            
            while (q.Count != 0)
            {
                var t = q.Dequeue();
                int now = t.Item1, f = t.Item2, cnt = t.Item3;
                
                if (cnt > corners) continue;
                if (now == v) {
                    res = new Tuple<int, int>(v, f);
                    break;
                    
                }
                if (!BlankCheck(now) && f != -1) continue;
                
                foreach (var i in Graph[now]) {
                    
                    int direct = GetDirect(now, i), add = 0;
                    if (direct != f) add = 1;
                    
                    if (cnt + add < min_corner[i, direct])
                    {
                        min_corner[i, direct] = cnt + add;
                        pre[i, direct] = new Tuple<int, int>(now, f);
                        q.Enqueue(new Tuple<int, int, int>(i, direct, cnt + add));
                    }
                }
            }
            
            if (res.Item1 != -1) {
                FindPath(u, res);
                return true;
            }
            return false;
        }

        public void FindPath(int u, Tuple<int, int> t) {
            Path.Push(t.Item1);
            if (u == t.Item1) return;
            FindPath(u, pre[t.Item1, t.Item2]);
        }
        
        public void LinkEmpty(int id)
        {
            BlankSet(id);
            int x = id / 10, y = id % 10;
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { 1, 0, -1, 0 };
            for (int i = 0; i < 4; i++) {
                int xx = x + dx[i], yy = y + dy[i];
                if (xx < 0 || xx >= rows || yy < 0 || yy >= columns) continue;
                int next = GetPos(xx, yy);
                Graph[id].AddLast(next);
                Graph[next].AddLast(id);
            }
        }
        
        public Stack<int> GetPath() { return Path; }

        public int[] Refresh()
        {
            refresh = new int[rows * columns];
            for (int i = 0; i < rows * columns; i++) refresh[i] = i;
            Random t = new Random();
            t.Shuffle(refresh);
            
            for (int i = 0; i < rows * columns; i++)
            {
                (GameMap[i], GameMap[refresh[i]]) = (GameMap[refresh[i]], GameMap[i]);
                (IsBlank[i], IsBlank[refresh[i]]) = (IsBlank[refresh[i]], IsBlank[i]);
            }

            BuildGraph();

            return refresh;
        }
    }
}

