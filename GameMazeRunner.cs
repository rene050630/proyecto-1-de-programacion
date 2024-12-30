using Game.Fichas;
using Game.Laberintos;
using Game.Casillas;
using Game.InterfazGrafica;
namespace Game.GameMazeRunners
{
    public class GameMazeRunner
    {
        private List <Ficha> FichasPlayer1;
        private List <Ficha> FichasPlayer2;
        private Laberinto laberinto;
        private Trampa trampa;
        public GameMazeRunner()
        {
            FichasPlayer1 = new List<Ficha> ();
            FichasPlayer2 = new List<Ficha> ();
            //InicializarFichas();
            //MostrarFichas();
            laberinto = new Laberinto (10, 10);
            trampa = new Trampa(laberinto);
            //Logica para indicar un movimiento valido
        }
        public void StartGame()
        {
            //logica para iniciar el juego
        }
        public void InicializarFichas()
        {
            FichasPlayer1.Add(new Ficha("Sprint", 1, Poder.sprint, 3, 1, laberinto));
            FichasPlayer1.Add(new Ficha("Fortaleza", 1, Poder.fortaleza, 4, 2, laberinto));
            FichasPlayer1.Add(new Ficha("Reloj de arena", 1, Poder.reloj, 3, 3, laberinto));
            FichasPlayer1.Add(new Ficha("Invisibilidad", 1, Poder.invisibilidad, 2, 4, laberinto));
            FichasPlayer1.Add(new Ficha("Saltadora", 1, Poder.saltar, 3, 5, laberinto));
            FichasPlayer2.Add(new Ficha("Retroceso", 1, Poder.retroceso, 2, 6, laberinto));
            FichasPlayer2.Add(new Ficha("Teletransporte", 1, Poder.teletransportarse, 5, 7, laberinto));
            FichasPlayer2.Add(new Ficha("Fuego", 1, Poder.destroyer, 6, 8, laberinto));
            FichasPlayer2.Add(new Ficha("Retraso", 1, Poder.retraso, 2, 9, laberinto));
            FichasPlayer2.Add(new Ficha("Intercambio", 1, Poder.intercambio, 5, 10, laberinto));
        }
        public void MostrarFichas()
        {
            System.Console.WriteLine("Selecciona la ficha que desee escrbiendo su número correspondiente");
            System.Console.WriteLine("1 - Nombre : Sprint, Velocidad : 1, Poder : Avanzar 4 casillas, Tiempo de enfriamiento : 3");
            System.Console.WriteLine("2 - Nombre : Fortaleza, Velocidad : 1, Poder : Avanzar por encima de obstaculos, Tiempo de enfriamiento : 4");
            System.Console.WriteLine("3 - Nombre : Reloj de arena, Velocidad : 1, Poder : Extender el tiempo de enfriamiento de otra ficha, Tiempo de enfriamiento : 3");
            System.Console.WriteLine("4 - Nombre : Invisibilidad, Velocidad : 1, Poder : Evita trampas, Tiempo de enfriamiento : 2");
            System.Console.WriteLine("5 - Nombre : Saltadora, Velocidad : 1, Poder : Puede saltar obstaculos y trampas, Tiempo de enfriamiento : 3");
            SeleccionarFichas (1);
            System.Console.WriteLine("Selecciona la ficha que desee escrbiendo su número correspondiente");
            System.Console.WriteLine("6 - Nombre : Retroceso, Velocidad : 1, Poder : Puede anular su movimiento, Tiempo de enfriamiento : 2");
            System.Console.WriteLine("7 - Nombre : Teletransporte, Velocidad : 1, Poder : Puede teletransportarse a cualquier lugar del laberinto, Tiempo de enfriamiento : 5");
            System.Console.WriteLine("8 - Nombre : Fuego, Velocidad : 1, Poder : Puede destruir obstaculos, Tiempo de enfriamiento : 3");
            System.Console.WriteLine("9 - Nombre : Retraso, Velocidad : 1, Poder : Hacer retroceder a otro jugador una casilla, Tiempo de enfriamiento : 2");
            System.Console.WriteLine("10 - Nombre : Intercambio, Velocidad : 1, Poder : Permite intercambiar su posicion con la de otra ficha cualquiera, Tiempo de enfriamiento : 5");
            SeleccionarFichas(2);
        }
        public void SeleccionarFichas(int jugador)
        {
            if (jugador == 1)
            {
                int ficha1 = int.Parse(Console.ReadLine()??string.Empty);
                if (ficha1 < 1 || ficha1 > 5)
                {
                    System.Console.WriteLine("escribe un numero en el rango correspondiente");
                    SeleccionarFichas(1);
                }
                else 
                {
                    System.Console.WriteLine("Has seleccionado la ficha numero " + ficha1 + "!!");
                    FichasPlayer1.RemoveAll(item => item.numero != ficha1);
                    System.Console.WriteLine("Presiona una tecla para continuar");
                    Console.ReadKey();
                }
            }
            else if (jugador == 2)
            {
                int ficha2 = int.Parse(Console.ReadLine()??string.Empty);
                if (ficha2 < 6 || ficha2 > 10)
                {
                    System.Console.WriteLine("escribe un numero en el rango correspondiente");
                    SeleccionarFichas(2);
                }
                else 
                {
                    System.Console.WriteLine("Has seleccionado la ficha numero " + ficha2 + "!!");
                    FichasPlayer2.RemoveAll(item => item.numero != ficha2);
                    System.Console.WriteLine("Presiona una tecla para continuar");
                    Console.ReadKey();
                }
            }
        }
        
        public void EndGame()
        {
            bool ficha1 = FichasPlayer1.Any(item => item.numero == 1); //no necesariamente tiene que ser uno ni 6
            bool ficha2 = FichasPlayer2.Any(item => item.numero == 6);
            if (ficha1 == laberinto.laberinto[9,9])
            {
                System.Console.WriteLine("Jugador 1 ha ganado");
            }
            else if(ficha2 == laberinto.laberinto[9,9])
            {
                System.Console.WriteLine("Jugador 2 ha ganado");
            }
        }
    }
}

