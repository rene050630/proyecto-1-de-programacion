public class Trampa
{
    public Laberinto laberintos;
    public bool[,] mask;
    public int[,] maskInt;
    public Trampa(Laberinto laberintos)
    {
        this.laberintos = laberintos;
        mask = new bool[laberintos.Filas, laberintos.Columnas];
        maskInt = new int [mask.GetLength(0), mask.GetLength(1)];
        SetTrap();
    }
    public bool[,] SetTrap()
    {
        NopuedeUsarPoder();
        mask[laberintos.Filas - 1, laberintos.Columnas - 1] = true;
        maskInt[laberintos.Filas - 1, laberintos.Columnas - 1] = 0;
        mask[0,0] = true;
        maskInt[0,0] = 0;
        // for (int i = 0; i < maskInt.GetLength(0); i++)
        // {
        //     for (int j = 0; j < maskInt.GetLength(1); j++)
        //     {
        //         System.Console.Write(maskInt[i,j] + "\t");
        //     }
        //     System.Console.WriteLine();
        // }
        // for (int i = 0; i < mask.GetLength(0); i++)
        //     {
        //         for (int j = 0; j < mask.GetLength(1); j++)
        //         {
        //             if (!mask[i,j])
        //             {
        //                 System.Console.Write(" ■ ");
        //             }
        //             else
        //             {
        //                 System.Console.Write(" □ ");
        //             }
        //         }
        //         System.Console.WriteLine();
        //     }
        return mask;
    }
    public bool[,] obstaculos()
    {
        bool [,] maskwalls = new bool[laberintos.Filas, laberintos.Columnas];
        int[] df = [-1, 1, 0, 0, -1, 1, -1, 1];
        int[] dc = [0, 0, 1, -1, -1, -1, 1, 1];
        for (int i = 0; i < laberintos.Filas; i ++)
        {
            for (int j = 0; j < laberintos.Columnas; j ++)
            {
                maskwalls[i,j] = laberintos.laberinto[i,j]; 
            }
        }
        int count = 0;
        int countpos = 0;
        for (int f = 0; f < laberintos.Filas; f ++)
        {
            for (int c = 0; c < laberintos.Columnas; c ++)
            {
                if (!maskwalls[f, c]) continue;
                for (int k = 0; k < df.Length; k ++)
                {
                    int vf = f + df[k];
                    int vc = c + dc[k];
                    if (!PosVal(vf, vc, laberintos.Filas, laberintos.Columnas)) 
                    {
                        countpos ++;
                        continue;
                    }
                    if (maskwalls[vf, vc]) count ++;
                }
                if (count + countpos == 8)
                {
                    maskwalls [f, c] = false;
                }
                count = 0;
                countpos = 0;
            }
        }
        maskwalls[0,0] = true;
        maskwalls[laberintos.Filas - 1, laberintos.Columnas - 1] = true;
            // for (int i = 0; i < maskwalls.GetLength(0); i++)
            // {
            //     for (int j = 0; j < maskwalls.GetLength(1); j++)
            //     {
            //         if (!maskwalls[i,j])
            //         {
            //             System.Console.Write(" ■ ");
            //         }
            //         else
            //         {
            //             System.Console.Write(" □ ");
            //         }
            //     }
            //     System.Console.WriteLine();
            // }
        return maskwalls;
    } 
        private static bool PosVal(int vf, int vc, int filas, int columnas)
        {
            return vf < filas && vf >= 0 && vc < columnas && vc >= 0;
        }
        public int[,] Begin()
        {
            mask = obstaculos();
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
                    if (count == 2) return maskInt;
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
                    if (count == 2) return maskInt;
                }
            }
            return maskInt;
        }
        
        public int[,] NopuedeUsarPoder()
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
                    if (count == 2) return maskInt;
                }
            }
            return maskInt;
        }
    }