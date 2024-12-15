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
            for (int i = 0; i < Filas; i++)
            {
                for (int j = 0; j < Columnas; j++)
                {
                    System.Console.Write(laberinto[i,j] + "\t ");
                }
                System.Console.WriteLine();
            }
        }
    }
}

