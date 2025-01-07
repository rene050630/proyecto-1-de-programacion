public class Trap
{
    public Maze Maze;
    public bool[,] mask;
    public int[,] maskInt;
    public bool[,] maskObs;
    public Trap(Maze Maze)
    {
        this.Maze = Maze;
        mask = new bool[Maze.Rows, Maze.Columns];
        maskInt = new int [mask.GetLength(0), mask.GetLength(1)];
        maskObs = new bool [Maze.Rows, Maze.Columns];
        SetTrap();
    }
    public bool[,] SetTrap()
    {
        Obstacle();
        CannotUsePower();
        mask[Maze.Rows - 1, Maze.Columns - 1] = true;
        maskInt[Maze.Rows - 1, Maze.Columns - 1] = 0;
        mask[0,0] = true;
        maskInt[0,0] = 0;
        for (int i = 0; i < maskInt.GetLength(0); i++)
        {
            for (int j = 0; j < maskObs.GetLength(1); j++)
            {
                System.Console.Write(maskInt[i,j] + "\t");
            }
            System.Console.WriteLine();
        }
        return mask;
    }
    public void Obstacle()
    {
        int[] df = [-1, 1, 0, 0, -1, 1, -1, 1];
        int[] dc = [0, 0, 1, -1, -1, -1, 1, 1];
        for (int i = 0; i < Maze.Rows; i ++)
        {
            for (int j = 0; j < Maze.Columns; j ++)
            {
                maskObs[i,j] = Maze.maze[i,j]; 
            }
        }
        int count = 0;
        int countpos = 0;
        for (int f = 0; f < Maze.Rows; f ++)
        {
            for (int c = 0; c < Maze.Columns; c ++)
            {
                if (!maskObs[f, c]) continue;
                for (int k = 0; k < df.Length; k ++)
                {
                    int vf = f + df[k];
                    int vc = c + dc[k];
                    if (!PosVal(vf, vc, Maze.Rows, Maze.Columns)) 
                    {
                        countpos ++;
                        continue;
                    }
                    if (maskObs[vf, vc]) count ++;
                }
                if (count + countpos == 8)
                {
                    maskObs [f, c] = false;
                }
                count = 0;
                countpos = 0;
            }
        }
        maskObs[0,0] = true;
        maskObs[Maze.Rows - 1, Maze.Columns - 1] = true;
    } 
        private static bool PosVal(int vf, int vc, int rows, int columns)
        {
            return vf < rows && vf >= 0 && vc < columns && vc >= 0;
        }
        public int[,] Begin()
        {
            for (int i = 0; i < Maze.Rows; i ++)
            {
                for (int j = 0; j < Maze.Columns; j ++)
                {
                    mask[i,j] = maskObs[i,j]; 
                }
            }
            for(int i = 0; i < mask.GetLength(0); i++)
            {
                for (int j = 0; j < mask.GetLength(1); j++)
                {
                    if (!mask[i,j])
                    {
                        maskInt[i,j] = 1;
                    }
                }
            }
            int count = 0;
            for (int i = mask.GetLength(0) - 1; i > 1; i --)
            {
                for (int j = mask.GetLength(1) - 1; j > 1; j --)
                {
                    if (maskInt [i,j] == 0)
                    {
                        maskInt[i,j] = 2;
                        mask [i,j] = false; 
                        count ++;
                    }
                    if (count == 4) return maskInt;
                }
            }
            return maskInt;
        }
        public int[,] Delay()
        {
            maskInt = Begin();
            int count = 0;
            for (int i = mask.GetLength(0) - 1; i > 0; i --)
            {
                for (int j = mask.GetLength(1) - 1; j > 0; j --)
                {
                    if (maskInt [i,j] == 0)
                    {
                        mask [i, j] = false;
                        maskInt[i,j] = 3;
                        count ++;
                    }
                    if (count == 4) return maskInt;
                }
            }
            return maskInt;
        }
        
        public int[,] CannotUsePower()
        {
            maskInt = Delay();
            int count = 0;
            for (int i =  mask.GetLength(0) - 1; i > 1; i --)
            {
                for (int j = mask.GetLength(1) - 1; j > 1; j --)
                {
                    if (maskInt [i,j] == 0)
                    {
                        mask [i, j] = false;
                        maskInt[i,j] = 4;
                        count ++;
                    }
                    if (count == 4) return maskInt;
                }
            }
            return maskInt;
        }
    }