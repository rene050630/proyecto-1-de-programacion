
public class Jugador
{
    public Ficha ficha;
    public string nombre;
    public Jugador(string nombre)
    {
        this.nombre = nombre;
        ficha = new Ficha("", 1, 3, Poderes.sprint, 1);
    }
}