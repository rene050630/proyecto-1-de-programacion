namespace Game.Fichas
{
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
}
