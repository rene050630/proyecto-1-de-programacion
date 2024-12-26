namespace Game.Fichas
{
    public class Ficha
    {
        string nombre {get; set;}
        int velocidad {get; set;}
        string poder{get;set;}
        int TiempoDeEnfriamiento {get;set;}
        int TurnosDeRecarga{get;set;}
        public int numero;
        public Ficha (string nombre, int velocidad, string poder, int TiempoDeEnfriamiento, int numero)
        {
            this.nombre = nombre;
            this.velocidad = velocidad;
            this.poder = poder;
            this.TiempoDeEnfriamiento = TiempoDeEnfriamiento;
            TurnosDeRecarga = 0;
            this.numero = numero;
        }
        public bool EstaEnPos(int x, int y)
        {
            return true;
        }
        public void ColocarFicha(int x, int y)
        {
            
        }
        public bool PuedeUsarPoder()
        {
            return TurnosDeRecarga < TiempoDeEnfriamiento;
        }
        public void NopuedeUsarPoder()
        {
            TiempoDeEnfriamiento = 5;
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
        public bool MovimientoValido(int x, int y)
        {
            //Logica para indicar un movimiento valido
            return true;
        }
        public void MovimientoDeFichas()
        {
            //if (MovimientoValido(x, y))
        }
        
        public void Turnos()
        {
            //logica para los turnos usando IncrementarTurno
            //Condicion de victoria
        }
    }
}
