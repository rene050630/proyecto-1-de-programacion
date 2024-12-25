using Game.Laberintos;
using Game.Fichas;
using Game.GameMazeRunners;
using System.ComponentModel;
using System.Reflection;
namespace Game.Casillas
{
    public class Trampa
    {
        public enum TipoDeTrampa {Begin, Delay, NopuedeUsarPoder}
        public TipoDeTrampa tipoDeTrampa { get; set; }
        private Laberinto laberintos;
        public bool [,] mask;
        public Trampa(TipoDeTrampa tipodeTrampa, Laberinto laberintos, List<Ficha> fichas)
        {
            tipoDeTrampa = tipodeTrampa;
            this.laberintos = laberintos;
        }
        public void Begin()
        {
            for (int i = 0; i < laberintos.Filas; i ++)
            {
                for (int j = 0; j < laberintos.Columnas; j ++)
                {
                    mask[i,j] = laberintos.laberinto[i,j];
                }
            }
            int count = 0;
            Ficha ficha = new Ficha("Sprint", 2, "Avanzar 4 casillas", 3);
            for (int i = mask.GetLength(0) - 1; i > 0; i ++)
            {
                for (int j = mask.GetLength(1) - 1; j > 0; j ++)
                {
                    if (mask [i,j])
                    {
                        if (ficha.EstaEnPos(i ,j)) ficha.ColocarFicha(0,0);
                        count ++;
                        mask [i, j] = false;
                    }
                }
                if (count == 2) return;
            }
        }
        public void Delay()
        {
            Ficha ficha = new Ficha("Sprint", 2, "Avanzar 4 casillas", 3);
            int count = 0;
            for (int i = mask.GetLength(0) - 1; i > 0; i ++)
            {
                for (int j = mask.GetLength(1) - 1; j > 0; j ++)
                {
                    if (mask [i,j])
                    {
                        if (ficha.EstaEnPos(i ,j))
                        {
                            if (ficha.MovimientoValido(i - 3, j - 2))
                            {
                                ficha.ColocarFicha(i - 3, j - 2);
                            }
                            else ValPos(i - 3, j - 2, mask, ficha);
                        }
                        count ++;
                        mask [i, j] = false;
                    }
                }
                if (count == 2) return;
            }
        }
        private void ValPos (int i, int j, bool [,] mask, Ficha ficha)
        {
            for (int k = 1; k < mask.GetLength(1); k ++)
            {
                if (mask [k + 1, k]) ficha.ColocarFicha(k + 1, k);
            }
        }
        public void NopuedeUsarPoder()
        {
            int count = 0;
            Ficha ficha = new Ficha("Sprint", 2, "Avanzar 4 casillas", 3);
            for (int i = mask.GetLength(0) - 1; i > 0; i ++)
            {
                for (int j = mask.GetLength(1) - 1; j > 0; j ++)
                {
                    if (mask [i,j])
                    {
                        if (ficha.EstaEnPos(i ,j)) ficha.NopuedeUsarPoder();
                        count ++;
                        mask [i, j] = false;
                    }
                }
                if (count == 2) return;
            }
        }
    }
}
