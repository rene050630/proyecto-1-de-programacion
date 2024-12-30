using Game.Laberintos;
using Game.Fichas;
using Game.GameMazeRunners;
using System.ComponentModel;
using System.Reflection;
using System.Net.Quic;
using System.Security.Cryptography.X509Certificates;
namespace Game.Casillas
{
    public enum TipoDeTrampa {Begin, Delay, NopuedeUsarPoder, obstaculos}
    public class Trampa
    {
        public TipoDeTrampa tipoDeTrampa { get; set; }
        private Laberinto laberintos;
        private bool[,] mask;
        public Trampa(Laberinto laberintos)
        {
            this.laberintos = laberintos;
            mask = new bool[laberintos.Filas, laberintos.Columnas];
            SetTrap();
        }
        public bool[,] SetTrap()
        {
            NopuedeUsarPoder();
            mask [0,0] = true;
            mask [laberintos.Filas - 1, laberintos.Columnas - 1] = true;
            // for (int i = 0; i < mask.GetLength(0); i++)
            // {
            //     for (int j = 0; j < mask.GetLength(1); j++)
            //     {
            //         if (!mask[i,j])
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
            return mask;
        }
        public void CaerEnTrampa(Ficha fichaPlayer1, Ficha fichaPlayer2, int x, int y)
        {
            if (fichaPlayer1.posicion == (x,y))
            {
                switch(tipoDeTrampa)
                {
                    case TipoDeTrampa.obstaculos:
                    obstaculos();
                    break;
                    case TipoDeTrampa.Begin :
                    fichaPlayer1.ColocarFicha(0, 0);
                    break;
                    case TipoDeTrampa.Delay :
                    if (fichaPlayer1.MovimientoValido(x - 3, y - 2))
                    {
                        fichaPlayer1.ColocarFicha(x - 3, y - 2);
                    }
                    else ValPos(x - 3, y - 2, mask, fichaPlayer1);
                    break;
                    case TipoDeTrampa.NopuedeUsarPoder :
                    if (fichaPlayer1.EstaEnPos(x ,y))
                    {
                        fichaPlayer1.NopuedeUsarPoder();
                    }
                    break;
                }
            }
            if (fichaPlayer2.posicion == (x,y))
            {
                switch(tipoDeTrampa)
                {
                    case TipoDeTrampa.obstaculos:
                    obstaculos();
                    break;
                    case TipoDeTrampa.Begin :
                    fichaPlayer2.ColocarFicha(0, 0);
                    break;
                    case TipoDeTrampa.Delay :
                    if (fichaPlayer2.MovimientoValido(x - 3, y - 2))
                    {
                        fichaPlayer2.ColocarFicha(x - 3, y - 2);
                    }
                    else ValPos(x - 3, y - 2, mask, fichaPlayer2);
                    break;
                    case TipoDeTrampa.NopuedeUsarPoder :
                    if (fichaPlayer2.EstaEnPos(x ,y))
                    {
                        fichaPlayer2.NopuedeUsarPoder();
                    }
                    break;
                }
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
                        maskwalls [f, c] = false;
                    }
                    count = 0;
                    countpos = 0;
                }
            }
            maskwalls[0,0] = true;
            maskwalls[laberintos.Filas - 1, laberintos.Columnas - 1] = true;
            for (int i = 0; i < maskwalls.GetLength(0); i++)
            {
                for (int j = 0; j < maskwalls.GetLength(1); j++)
                {
                    if (!maskwalls[i,j])
                    {
                        System.Console.Write(" ■ ");
                    }
                    else
                    {
                        System.Console.Write(" □ ");
                    }
                }
                System.Console.WriteLine();
            }
            return maskwalls;
        }
        //Despues de poner todas las trampas devolver la mascara booleana
        //Como especificar la ficha que caiga en una trampa 
        private static bool PosVal(int vf, int vc, int filas, int columnas)
        {
            return vf < filas && vf >= 0 && vc < columnas && vc >= 0;
        }
        public bool[,] Begin()
        {
            mask = obstaculos();
            int count = 0;
            for (int i = mask.GetLength(0) - 1; i > 0; i --)
            {
                for (int j = mask.GetLength(1) - 1; j > 0; j --)
                {
                    if (mask [i,j])
                    {
                        mask [i, j] = false; 
                        if (Pos(mask, i, j)) continue;
                        count ++;
                    }
                    //if (count == 2) return mask;
                }
            }
            return mask;
        }
        public bool[,] Delay()
        {
            mask = Begin();
            int count = 0;
            for (int i = mask.GetLength(0) - 1; i > 0; i --)
            {
                for (int j = mask.GetLength(1) - 1; j > 0; j --)
                {
                    if (mask [i,j])
                    {
                        mask [i, j] = false;
                        if (Pos(mask, i, j)) continue;
                    }
                    count ++;
                    //if (count == 2) return mask;
                }
            }
            return mask;
        }
        private void ValPos (int i, int j, bool [,] mask, Ficha ficha)
        {
            for (int k = 1; k < mask.GetLength(1); k ++)
            {
                if (mask [k + 1, k]) ficha.ColocarFicha(k + 1, k);
            }
        }
        public bool [,] NopuedeUsarPoder()
        {
            mask = Delay();
            int count = 0;
            for (int i = 1; i < mask.GetLength(0) - 1; i ++)
            {
                for (int j = 1; j < mask.GetLength(1) - 1; j ++)
                {
                    if (mask [i,j])
                    {
                        mask [i, j] = false;
                        if (Pos(mask, i, j)) continue;
                        count ++;
                    }
                    //if (count == 2) return mask;
                }
            }
            return mask;
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
