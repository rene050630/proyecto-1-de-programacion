using Game.Casillas;
using Game.GameMazeRunners;
using Game.Laberintos;
namespace Game.Fichas
{
    public enum Poder{sprint, fortaleza, reloj, invisibilidad, saltar, retroceso, teletransportarse, destroyer, retraso, intercambio};
    public class Ficha
    {
        string nombre {get; set;}
        int velocidad {get; set;}
        public Poder poderFicha {get; set;}
        int TiempoDeEnfriamiento {get;set;}
        int TurnosDeRecarga{get;set;}
        public int numero;
        Trampa trampa;
        Laberinto laberintos;
        public Ficha (string nombre, int velocidad, Poder poderFicha, int TiempoDeEnfriamiento, int numero, Laberinto laberintos)
        {
            this.nombre = nombre;
            this.velocidad = velocidad;
            this.poderFicha = poderFicha;
            this.TiempoDeEnfriamiento = TiempoDeEnfriamiento;
            TurnosDeRecarga = 0;
            this.numero = numero;
            trampa = new Trampa(TipoDeTrampa.obstaculos, laberintos);
            
        }
        public void Sprint(int x, int y)
        {
            int [,] distancias = new int [laberintos.Filas, laberintos.Columnas];
            CaminoFast(distancias, x, y);
            TiempoDeEnfriamiento = 3;
        }
        public void Fortaleza ()
        {
            bool[,] mask = trampa.obstaculos();
            for (int i = 0; i < mask.GetLength(0); i++)
            {
                for (int j = 0; j < mask.GetLength(1); j++)
                {
                    if (!mask[i,j])
                    {
                        if (EstaEnPos(i - 1, j) || EstaEnPos(i + 1, j) || EstaEnPos(i, j - 1) || EstaEnPos(i, j + 1))
                        {
                            ColocarFicha(i + 1, j); //posible switch
                            TiempoDeEnfriamiento = 4;
                        }
                    }
                }
            }
        }
        public void Reloj()
        {
            switch(poderFicha) //duda en que poner en el switch
            {
                case Poder.retroceso:
                TiempoDeEnfriamiento = 6;
                break;
                case Poder.teletransportarse:
                TiempoDeEnfriamiento = 6;
                break;
                case Poder.destroyer:
                TiempoDeEnfriamiento = 6;
                break;
                case Poder.retraso:
                TiempoDeEnfriamiento = 6;
                break;
                case Poder.intercambio:
                TiempoDeEnfriamiento = 6;
                break;
            }
            TiempoDeEnfriamiento = 3;
        }
        public void Invisibilidad()
        {
            bool[,] mask = trampa.obstaculos();  
            for (int i = 0; i < mask.GetLength(0); i++)
            {
                for (int j = 0; j < mask.GetLength(1); j++)
                {
                    if (mask[i,j])
                    {
                        if (EstaEnPos(i - 1, j) || EstaEnPos(i + 1, j) || EstaEnPos(i, j - 1) || EstaEnPos(i, j + 1))
                        {
                            ColocarFicha(i + 1, j); //posible switch
                            TiempoDeEnfriamiento = 4;
                        }
                    }
                }
            }
        }
        public void Saltadora()
        {
            bool[,] mask = trampa.NopuedeUsarPoder(); //tiene que devolver mascara booleana con las trampas  
            for (int i = 0; i < mask.GetLength(0); i++)
            {
                for (int j = 0; j < mask.GetLength(1); j++)
                {
                    if (!mask[i,j])
                    {
                        if (EstaEnPos(i - 1, j) || EstaEnPos(i + 1, j) || EstaEnPos(i, j - 1) || EstaEnPos(i, j + 1))
                        {
                            ColocarFicha(i + 1, j); //posible switch
                            TiempoDeEnfriamiento = 5;
                        }
                    }
                }
            }
        }
        public void Retroceso()
        {
            //Como hacer una pila con esta sola ficha
            TiempoDeEnfriamiento = 2;
        }
        public void Destroyer()
        {
            bool[,] mask = trampa.obstaculos(); //hacer mascara booleana con solo obstaculos
            for (int i = 0; i < mask.GetLength(0); i ++)
            {
                for (int j = 0; j < mask.GetLength(1); j ++)
                {
                    if (!mask[i,j])
                    {
                        if (EstaEnPos(i - 1, j) || EstaEnPos(i + 1, j) || EstaEnPos(i, j - 1) || EstaEnPos(i, j + 1))
                        {
                            mask[i,j] = true; 
                            TiempoDeEnfriamiento = 6;
                        }
                    }
                }
            }
        }
        public void Retraso()
        {
            //Pasarle como parametro la ficha del player 1
            if (MovimientoDeFichas(fichaplayer1).Count > 0) 
            { 
                (fichaX, fichaY) = MovimientoDeFichas(fichaplayer1).Pop(); 
                Console.WriteLine($"Movimiento deshecho. Ficha regresada a ({fichaX}, {fichaY})");
                TiempoDeEnfriamiento = 2; 
            } 
            else 
            { 
                Console.WriteLine("No hay movimientos para deshacer."); 
            }
        }
        public void Intercambio()
        {
            //Intercambiar posiciones entre la ficha del player 1 y la del player 2
        }
        public void Invi()
        {
            //OtroPoder
        }
        private static bool PosVal(int vf, int vc, int filas, int columnas)
        {
            return vf < filas && vf >= 0 && vc < columnas && vc >= 0;
        }
        private void CaminoFast(int [,] distancia, int x, int y)
        {
            bool[,] mask = trampa.obstaculos();
            int [] df = [0, 1, 0, -1];
            int [] dc = [1, 0, -1, 0];
            int [,] distancias = new int [mask.GetLength(0), mask.GetLength(1)];
            distancias[x,y] = 1;
            bool hubocambio;
            do
            {
                hubocambio = false;
                for (int f = 0; f < mask.GetLength(0); f ++)
                {
                    for (int c = 0; c < mask.GetLength(1); c ++)
                    {
                        if (distancias[f,c] == 0) continue;
                        if (!mask[f,c]) continue;
                        for (int k = 0; k < df.Length; k++)
                        {
                            int vf = f + df[k];
                            int vc = c + dc[k];
                            if (PosVal(vf, vc, mask.GetLength(0), mask.GetLength(1)) && distancias[vf, vc] == 0 && mask[vf, vc])
                            {
                                distancias[vf, vc] = distancias[f,c] + 1;
                                if (distancia[vf, vc] - distancia[x,y] == 4)
                                {
                                    ColocarFicha(vf, vc);
                                }
                                hubocambio = true;
                                break;
                            }
                        }
                    }
                }
            } while(hubocambio);
        }
        public bool EstaEnPos(int x, int y)
        {
            //duda
            return true;
        }
        public void ColocarFicha(int x, int y)
        {
            //duda
        }
        public bool PuedeUsarPoder()
        {
            return TurnosDeRecarga >= TiempoDeEnfriamiento;
        }
        public void NopuedeUsarPoder()
        {
            TiempoDeEnfriamiento = 5;
        }
        public void UsarPoder()
        {
            if (PuedeUsarPoder()) 
            {
                switch(poderFicha)
                {
                    case Poder.sprint:
                    Sprint(x, y); //Pasar como parametro posicion actual
                    break;
                    case Poder.destroyer:
                    Destroyer();
                    break;
                    case Poder.fortaleza:
                    Fortaleza();
                    break;
                    case Poder.reloj:
                    Reloj();
                    break;
                    case Poder.invisibilidad:
                    Invisibilidad();
                    break;
                    case Poder.retraso:
                    Retraso();
                    break;
                    case Poder.retroceso:
                    Retroceso();
                    break;
                    case Poder.saltar:
                    Saltadora();
                    break;
                    case Poder.intercambio:
                    Intercambio();
                    break;
                }
                TurnosDeRecarga = 0;
            }
        }
        public void IncrementarTurno()
        {
            TurnosDeRecarga ++;
        }
        public bool MovimientoValido(int x, int y)
        {
            //Logica para indicar un movimiento valido
            return true;
        }
        public Stack<(int, int)> MovimientoDeFichas(int x, int y)
        {
            if (fichaplayer1) //como definir una sola ficha para los players
            {
                Stack<(int, int)> movimientosficha1 = new Stack<(int, int)>();
                if (laberintos.laberinto[x, y]) //Duda en si poner laberinto o casilla y que pasarle como paremetro 
                { 
                    movimientosficha1.Push((fichaX, fichaY));
                    fichaX = x; 
                    fichaY = y;
                }
                return movimientosficha1;
            }
            if (fichaplayer2)
            {
                Stack<(int, int)> movimientosficha2 = new Stack<(int, int)>();
                if (laberintos.laberinto[x, y]) //Duda en si poner laberinto o casilla y que pasarle como paremetro 
                { 
                    movimientosficha2.Push((fichaX, fichaY));
                    fichaX = x; 
                    fichaY = y;
                }
                return movimientosficha2;
            }
            //if (MovimientoValido(x, y))
        }
        
        public void Turnos()
        {
            //logica para los turnos usando IncrementarTurno
            //Condicion de victoria
        }
    }
}
