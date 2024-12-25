namespace Game.InterfazGrafica
{
    internal class Vetana(int ancho, int largo)
    {
        int ancho = ancho;
        int largo = largo;
        private void Init()
        {
            Console.SetWindowSize(ancho, largo);
        }
        // public void ShowMenu()
        // {
        //     //muestra el menu
        // }
        // public void ShowLaberinto()
        // {
        //     //muestra interfaz del juego
        // }
        // public void Victoria()
        // {
        //     //Muestra pantalla de victoria
        // }
        // public void Update()
        // {
        //     //actualiza la interfaz
        // }
    }
}
