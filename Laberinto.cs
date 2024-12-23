using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Game.Casillas;

namespace Game.Laberintos
{
    public class Laberinto 
    {
        private bool [,] laberinto;
        public int Filas;
        public int Columnas;
        public Laberinto (int filas, int columnas)
        {
            Filas = filas;
            Columnas = columnas;
            laberinto = new bool [filas, columnas];
            GenerarLaberinto ();
            System.Console.WriteLine("holas");
            ColocarParedes(filas, columnas, laberinto);
        }
        public void GenerarLaberinto()
        {
            Random random = new Random();
            for (int i = 0; i < Filas; i++)
            {
                for (int j = 0; j < Columnas; j++)
                {
                    laberinto [i,j] = random.Next(2) == 1;
                }
            }
            EsValido(Filas, Columnas, laberinto);
        }
        private static void EsValido(int filas, int columnas, bool[,] laberinto)
        {
            int[] df = [0, 1, 0, -1];
            int[] dc = [1, 0, -1, 0];
            laberinto [0,0] = true;
            for (int k = 0; k < df.Length; k++)
            {
                for (int f = 0; f < filas; f ++)
                {
                    for (int c = 0; c < columnas; c ++)
                    {
                        int vf = f + df[k]*f;
                        int vc = c + dc[k]*f;
                        if (!PosVal(vf, vc, filas, columnas)) continue;
                        if (laberinto [vf, vc]) DoVal(laberinto, vf, vc, filas, columnas);
                    }
                }
            }
            laberinto [filas - 1, columnas - 1] = true;
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    System.Console.Write(laberinto[i,j] + "\t ");
                }
                System.Console.WriteLine();
            }
        }
        private static bool PosVal(int vf, int vc, int filas, int columnas)
        {
            return vf < filas && vf >= 0 && vc < columnas && vc >= 0;
        }
        private static void DoVal(bool [,] laberinto, int actualfila, int actualcolumna, int filas, int columnas)
        {
            int[] df = [0, 1];
            int[] dc = [1, 0];
            for (int k = 0; k < df.Length; k ++)
            {
                int vf = actualfila + df[k];
                int vc = actualcolumna + dc[k];
                if (!PosVal(vf, vc, filas, columnas)) continue;
                if (laberinto [vf, vc]) return;
                else if (!laberinto [vf, vc])
                {
                    laberinto [vf, vc] = true;
                }
            }
            //Fila0(laberinto, filas, columnas);                
        }
        // private static void Fila0(bool [,] laberinto, int filas, int columnas)
        // {
        //     int count = 0;
        //     for (int i = 0; i < filas; i++)
        //     {
        //         for (int j = 0; j < columnas; j ++)
        //         {
        //             if (!laberinto [i, j]) count ++;
        //         }
        //         if (count == filas - 1)
        //         {
        //             laberinto [i, count] = true;
        //         }
        //         count = 0;
        //     }
        // }
        private static void ColocarParedes(int filas, int columnas, bool[,] laberinto)
        {
            bool [,] mask = new bool[filas, columnas];
            int[] df = [-1, 1, 0, 0, -1, 1, -1, 1];
            int[] dc = [0, 0, 1, -1, -1, -1, 1, 1];
            for (int i = 0; i < filas; i ++)
            {
                for (int j = 0; j < columnas; j ++)
                {
                    mask[i,j] = laberinto[i,j]; 
                }
            }
            int count = 0;
            int countpos = 0;
            for (int f = 0; f < filas; f ++)
            {
                for (int c = 0; c < columnas; c ++)
                {
                    if (!mask[f, c]) continue;
                    for (int k = 0; k < df.Length; k ++)
                    {
                        int vf = f + df[k];
                        int vc = c + dc[k];
                        if (!PosVal(vf, vc, filas, columnas)) 
                        {
                            countpos ++;
                            continue;
                        }
                        if (mask[vf, vc]) count ++;
                    }
                    if (count + countpos == 8)
                    {
                        Casilla Obstaculo = new(f, c, Casilla.TipoDecasilla.obstaculo);
                        mask [f, c] = false;
                    }
                    count = 0;
                    countpos = 0;
                }
            }
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    System.Console.Write(mask[i,j] + "\t ");
                }
                System.Console.WriteLine();
            }
        }
    }
}

