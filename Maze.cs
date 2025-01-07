
    public class Maze 
    {
        public bool [,] maze;
        public int Rows;
        public int Columns;
        public Player player1;
        public Player player2;
        public Maze (int rows, int columns, Player player1, Player player2)
        {
            Rows = rows;
            Columns = columns;
            maze = new bool [rows, columns];
            this.player1 = player1;
            this.player2 = player2;
            GenerateMaze ();
            PrintMaze();
        }
        public void GenerateMaze()
        {
            Random random = new Random();
            for (int i = 1; i < Rows; i++)
            {
                for (int j = 1; j < Columns - 1; j++)
                {
                    maze [i,j] = random.Next(2) == 1;
                }
            }
            IsValid(Rows, Columns, maze);
        }
        private static void IsValid(int rows, int columns, bool[,] maze)
        {
            maze [0,0] = true;
            for (int f = 0; f < rows; f ++)
            {
                for (int c = 0; c < columns; c ++)
                {
                    if (maze [f, c]) DoVal(maze, f, c, rows, columns);
                }
            }
            maze [rows - 1, columns - 1] = true;
        }
        public void PrintMaze()
        {
            int count = 0;
            while(count < 1)
            {
                Console.Clear();
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (player1.token.position == (i,j) && player2.token.position == (i,j))
                        {
                            System.Console.Write("a b");
                            count ++;
                        }
                        else if (player1.token.position == (i,j))
                        {
                            System.Console.Write(" a ");
                            count ++;
                        }
                        else if (player2.token.position == (i,j))
                        {
                            System.Console.Write(" b ");
                            count ++;
                        }
                        else if (!maze[i,j])
                        {
                            System.Console.Write(" ■ ");
                            count ++;
                        }
                        else
                        {
                            System.Console.Write(" □ ");
                            count ++;
                        }
                        
                    }
                    System.Console.WriteLine();
                }
            }
        }
        private static bool PosVal(int vf, int vc, int rows, int columns)
        {
            return vf < rows && vf >= 0 && vc < columns && vc >= 0;
        }
        private static void DoVal(bool [,] maze, int currentRow, int currentColumn, int rows, int columns)
        {
            int[] df = [0, 1];
            int[] dc = [1, 0];
            for (int k = 0; k < df.Length; k ++)
            {
                int vf = currentRow + df[k];
                int vc = currentColumn + dc[k];
                if (!PosVal(vf, vc, rows, columns)) continue;
                if (maze [vf, vc]) return;
                else if (!maze [vf, vc])
                {
                    maze [vf, vc] = true;
                }
            }               
        }
    }
