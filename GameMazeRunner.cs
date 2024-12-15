using Game.Fichas;
using Game.Laberintos;
using Game.Casillas;
namespace Game.GameMazeRunners
{
    public class GameMazeRunner
    {
        private List <Ficha> FichasPlayer1;
        private List <Ficha> FichasPlayer2;
        private Laberinto laberinto;
        public GameMazeRunner()
        {
            FichasPlayer1 = new List<Ficha> ();
            FichasPlayer2 = new List<Ficha> ();
            laberinto = new Laberinto (5, 5);
            InicializarFichas();
            //Logica para indicar un movimiento valido
            SeleccionarFichas(FichasPlayer1, FichasPlayer2);
        }
        public void StartGame()
        {
            //logica para iniciar el juego
        }
        public void InicializarFichas()
        {
            FichasPlayer1.Add(new Ficha("Sprint", 2, "Avanzar 4 casillas", 3));
            FichasPlayer1.Add(new Ficha("Fortaleza", 1, "Avanzar por encima de obstaculos", 4));
            FichasPlayer1.Add(new Ficha("Reloj de arena", 2, "Reducir el tiempo de enfriamiento de otra ficha", 3));
            FichasPlayer1.Add(new Ficha("Invisibilidad", 2, "Evita una trampa", 2));
            FichasPlayer1.Add(new Ficha("Saltadora", 1, "La ficha puede saltar por obstaculos y trampas", 3));
            FichasPlayer2.Add(new Ficha("Retroceso", 2, "Puede anular su movimiento", 2));
            FichasPlayer2.Add(new Ficha("Teletransporte", 1, "Puede teletransportarse a cualquier lugar del laberinto", 5));
            FichasPlayer2.Add(new Ficha("Fuego", 2, "Puede destruir obstaculos", 3));
            FichasPlayer2.Add(new Ficha("Retraso", 1, "hacer retroceder a otro jugador una casilla", 2));
            FichasPlayer2.Add(new Ficha("Intercambio", 1, "Permite intercambiar su posicion con la de otra ficha cualquiera", 5));
        }
        public void MostrarFichas()
        {
            foreach(var n in FichasPlayer1) System.Console.WriteLine(n);
            foreach(var a in FichasPlayer2) System.Console.WriteLine(a);
        }
        public void SeleccionarFichas(List <Ficha> FichasSeleccionadas1, List <Ficha> FichaSeleccionadas2)
        {
            //Logica de Seleccion de fichas de cada jugador
        }
        public void MovimientoDeFichas()
        {
            //if (MovimientoValido(x, y))
        }
        public bool MovimientoValido(int x, int y)
        {
            //Logica para indicar un movimiento valido
            return true;
        }
        public void Turnos()
        {
            //logica para los turnos usando IncrementarTurno
            //Condicion de victoria
        }
        public void EndGame()
        {
            //logica para finalizar el juego
        }
    }
}


