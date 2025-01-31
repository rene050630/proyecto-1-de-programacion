public class Window
{
    public void Menu()
        {
            System.Console.WriteLine("BIENVENIDO AL MAZE RUNNER 🛣🛣🛣");
            System.Console.WriteLine("Búsquese un amigo para jugar");
            System.Console.WriteLine("TEN CUIDADO CON LAS TRAMPAS Y LOS OBSTÁCULOS INVISIBLES");
            System.Console.WriteLine("(Enter) Inicio del juego");
            System.Console.WriteLine("(Esc) Salir");
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                for (int i = 3; i >= 0; i --)
                {
                    System.Console.WriteLine("El juego inicia en " + i);
                    Thread.Sleep(1000);
                }
                return;
            }
            else if(key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }
}
