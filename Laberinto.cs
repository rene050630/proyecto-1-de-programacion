
    public class Laberinto 
    {
        public bool [,] laberinto;
        public int Filas;
        public int Columnas;
        public Jugador jugador1;
        public Jugador jugador2;
        public Laberinto (int filas, int columnas, Jugador jugador1, Jugador jugador2)
        {
            Filas = filas;
            Columnas = columnas;
            laberinto = new bool [filas, columnas];
            this.jugador1 = jugador1;
            this.jugador2 = jugador2;
            GenerarLaberinto ();
            ImprimirLab();
        }
        public void GenerarLaberinto()
        {
            Random random = new Random();
            for (int i = 1; i < Filas; i++)
            {
                for (int j = 1; j < Columnas - 1; j++)
                {
                    laberinto [i,j] = random.Next(2) == 1;
                }
            }
            EsValido(Filas, Columnas, laberinto);
        }
        private static void EsValido(int filas, int columnas, bool[,] laberinto)
        {
            // int[] df = [0, 1, 0, -1];
            // int[] dc = [1, 0, -1, 0];
            laberinto [0,0] = true;
            //for (int k = 0; k < df.Length; k++)
            {
                for (int f = 0; f < filas; f ++)
                {
                    for (int c = 0; c < columnas; c ++)
                    {
                        // int vf = f + df[k]*f;
                        // int vc = c + dc[k]*f;
                        // if (!PosVal(vf, vc, filas, columnas)) continue;
                        // if (laberinto [vf, vc]) DoVal(laberinto, vf, vc, filas, columnas);
                        //if (!PosVal(i, vc, filas, columnas)) continue;
                        if (laberinto [f, c]) DoVal(laberinto, f, c, filas, columnas);
                    }
                }
            }
            laberinto [filas - 1, columnas - 1] = true;
        }
        public void ImprimirLab()
        {
            int count = 0;
            while(count < 1)
            {
                Console.Clear();
                for (int i = 0; i < Filas; i++)
                {
                    for (int j = 0; j < Columnas; j++)
                    {
                        if (jugador1.ficha.posicion == (i,j) && jugador2.ficha.posicion == (i,j))
                        {
                            System.Console.Write("a b");
                            count ++;
                        }
                        else if (jugador1.ficha.posicion == (i,j))
                        {
                            System.Console.Write(" a ");
                            count ++;
                        }
                        else if (jugador2.ficha.posicion == (i,j))
                        {
                            System.Console.Write(" b ");
                            count ++;
                        }
                        else if (!laberinto[i,j])
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
        }
    }
