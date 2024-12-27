using Game.Laberintos;
using Game.Fichas;
using Game.GameMazeRunners;
using System.ComponentModel;
using System.Reflection;
using System.Net.Quic;
namespace Game.Casillas
{
    public enum TipoDeTrampa {Begin, Delay, NopuedeUsarPoder, obstaculos}
    public class Trampa
    {
        public TipoDeTrampa tipoDeTrampa { get; set; }
        private Laberinto laberintos;
        private bool[,] mask;
        public Trampa(TipoDeTrampa tipodeTrampa, Laberinto laberintos)
        {
            tipoDeTrampa = tipodeTrampa;
            this.laberintos = laberintos;
            mask = new bool[laberintos.Filas, laberintos.Columnas];
        }
        public void SetTrap()
        {
            switch(tipoDeTrampa)
            {
                case TipoDeTrampa.obstaculos:
                obstaculos();
                break;
                case TipoDeTrampa.Begin :
                Begin();
                break;
                case TipoDeTrampa.Delay :
                Delay();
                break;
                case TipoDeTrampa.NopuedeUsarPoder :
                NopuedeUsarPoder();
                break;
            }
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
                        mask [f, c] = false;
                    }
                    count = 0;
                    countpos = 0;
                }
            }
            maskwalls[0,0] = true;
            maskwalls[laberintos.Filas - 1, laberintos.Columnas - 1] = true;
            return maskwalls;
        }
        //Despues de poner todas las trampas devolver la mascara booleana
        //Como especificar la ficha que caiga en una trampa
        //como hacer numeros finitos de trampas 
        private static bool PosVal(int vf, int vc, int filas, int columnas)
        {
            return vf < filas && vf >= 0 && vc < columnas && vc >= 0;
        }
        public void Begin()
        {
            bool [,] maskWalls = obstaculos();
            for (int i = 0; i < laberintos.Filas; i ++)
            {
                for (int j = 1; j < laberintos.Columnas; j ++)
                {
                    mask[i,j] = maskWalls[i,j];
                }
            }
            int count = 0;
            Ficha ficha = new("Sprint", 2, "Avanzar 4 casillas", 3, 1);
            for (int i = mask.GetLength(0) - 1; i >= 0; i --)
            {
                for (int j = mask.GetLength(1) - 2; j > 0; j --)
                {
                    if (mask [i,j])
                    {
                        mask [i, j] = false;
                        if (ficha.EstaEnPos(i ,j)) 
                        {
                            if (Pos(mask, i, j)) continue;
                            ficha.ColocarFicha(0,0);
                        }
                        count ++;
                    }
                }
                if (count == 2) return;
            }
        }
        public void Delay()
        {
            Ficha ficha = new("Sprint", 2, "Avanzar 4 casillas", 3, 1);
            int count = 0;
            for (int i = mask.GetLength(0) - 1; i >= 0; i --)
            {
                for (int j = mask.GetLength(1) - 1; j > 0; j --)
                {
                    if (mask [i,j])
                    {
                        mask [i, j] = false;
                        if (ficha.EstaEnPos(i ,j))
                        {
                            if (Pos(mask, i, j)) continue;
                            if (ficha.MovimientoValido(i - 3, j - 2))
                            {
                                ficha.ColocarFicha(i - 3, j - 2);
                            }
                            else ValPos(i - 3, j - 2, mask, ficha);
                        }
                        count ++;
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
            Ficha ficha = new Ficha("Sprint", 2, "Avanzar 4 casillas", 3, 1);
            for (int i = mask.GetLength(0) - 1; i > 0; i --)
            {
                for (int j = mask.GetLength(1) - 1; j > 0; j --)
                {
                    if (mask [i,j])
                    {
                        mask [i, j] = false;
                        if (ficha.EstaEnPos(i ,j))
                        {
                            if (Pos(mask, i, j)) continue;
                            ficha.NopuedeUsarPoder();
                        }
                        count ++;
                    }
                }
                if (count == 2) return;
            }
        }
        private static bool Pos(bool [,] mask, int a, int b)
        {
            int countf = 0;
            int countc = 0;
            for (int i = 0; i < mask.GetLength(0); i ++)
            {
                for (int j = 0; j < mask.GetLength(1); j ++)
                {
                    if (!mask [i,j]) countf ++;
                    if (!mask[j,i]) countc ++;
                }
                if (countf == mask.GetLength(0) - 1)
                {
                    mask[a,b] = true;
                    return true;
                }
                if (countc == mask.GetLength(0) - 1)
                {
                    mask[a,b] = true;
                    return true;
                }
                countf = 0;
                countc = 0;
            }
            return false;
        }
    }
}
