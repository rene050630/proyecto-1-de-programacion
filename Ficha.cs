    public enum Poderes{sprint, fortaleza, reloj, invisibilidad, saltar, retroceso, trampero, destroyer, retraso, intercambio};
    public class Ficha
    {
        public string nombre {get; set;}
        public int velocidad {get; set;}
        public int TiempoDeEnfriamiento {get;set;}
        public Poderes Poderficha;
        public bool IsActive;
        public (int, int) SafePos;
        public int TurnosDeRecarga;
        public int numero;
        public (int, int) posicion;
        public Stack <(int, int)> movimientos = new Stack<(int, int)>();
        public Ficha (string nombre, int velocidad, int TiempoDeEnfriamiento, Poderes PoderFicha ,int numero)
        {
            this.nombre = nombre;
            this.velocidad = velocidad;
            this.TiempoDeEnfriamiento = TiempoDeEnfriamiento;
            this.numero = numero;
            this.Poderficha = PoderFicha;
            TurnosDeRecarga = 0;
            posicion = (0, 0);
            SafePos = (0,0);
        }
        public bool EstaEnPos(int x, int y)
        {
            if (posicion.Item1 == x && posicion.Item2 == y) return true;
            return false;
        }
        public void ColocarFicha(int x, int y)
        {
            posicion.Item1 = x;
            posicion.Item2 = y;
        }
    }

