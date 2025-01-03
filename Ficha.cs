
    public class Ficha
    {
        public string nombre {get; set;}
        public int velocidad {get; set;}
        public int TiempoDeEnfriamiento {get;set;}
        public int numero;
        public (int, int) posicion;
        public Ficha (string nombre, int velocidad, int TiempoDeEnfriamiento, int numero)
        {
            this.nombre = nombre;
            this.velocidad = velocidad;
            this.TiempoDeEnfriamiento = TiempoDeEnfriamiento;
            this.numero = numero;
            posicion = (0, 0);
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
        public void NopuedeUsarPoder()
        {
            TiempoDeEnfriamiento = 5;
        }
    }

