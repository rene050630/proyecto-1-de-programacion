public class PrincipalClass
{
    static void Main ()
    {
        GameMazeRunner a = new GameMazeRunner();
    }
}
public class GameMazeRunner
{
    // private List <Ficha> FichasPlayer1;
    // private List <Ficha> FichasPlayer2;
    // private Laberinto laberinto;
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
public class Ficha
{
    string nombre {get; set;}
    int velocidad {get; set;}
    string poder{get;set;}
    int TiempoDeEnfriamiento {get;set;}
    int TurnosDeRecarga{get;set;}
    public Ficha (string nombre, int velocidad, string poder, int TiempoDeEnfriamiento)
    {
        this.nombre = nombre;
        this.velocidad = velocidad;
        this.poder = poder;
        this.TiempoDeEnfriamiento = TiempoDeEnfriamiento;
        TurnosDeRecarga = 0;
    }
    public bool PuedeUsarPoder()
    {
        if (TurnosDeRecarga < TiempoDeEnfriamiento) return false;
        else return true;
    }
    public void UsarPoder()
    {
        if (PuedeUsarPoder()) 
        {
            //Logica para el uso del poder
            TurnosDeRecarga = 0;
        }
    }
    public void IncrementarTurno()
    {
        TurnosDeRecarga ++;
    }
}
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
public class Casilla
{
    public enum TipoDecasilla {normal, obstaculo, trampa}
    public TipoDecasilla tipoDecasilla {get;set;}
    int X;
    int Y;
    public Casilla (int x, int y, TipoDecasilla tipoDecasilla)
    {
        X = x;
        Y = y;
        this.tipoDecasilla = tipoDecasilla;
    }
}

public class Trampa : Casilla
{
    public enum TipoDeTrampa {}
    public TipoDeTrampa tipoDeTrampa { get; set; }

    public Trampa(int x, int y, TipoDeTrampa tipodeTrampa) : base(x, y, TipoDecasilla.trampa)
    {
        tipoDeTrampa = tipodeTrampa;
    }

    // Métodos específicos para trampas
}
public class Obstaculo : Casilla
{
    public enum TipoDeObstaculo {}
    public TipoDeObstaculo tipoDeObstaculo { get; set; }
    public Obstaculo(int x, int y, TipoDeObstaculo tipodeObstaculo) : base(x, y, TipoDecasilla.obstaculo)
    {
        tipoDeObstaculo = tipodeObstaculo;
    }
    //Metodos para crear obstaculos
}
public class Interfaz
{
    public void ShowMenu()
    {
        //muestra el menu
    }
    public void ShowLaberinto()
    {
        //muestra interfaz del juego
    }
    public void Victoria()
    {
        //Muestra pantalla de victoria
    }
    public void Update()
    {
        //actualiza la interfaz
    }
}