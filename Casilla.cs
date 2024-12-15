namespace Game.Casillas
{
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
}
