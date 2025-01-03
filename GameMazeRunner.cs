
    public class GameMazeRunner
    {
        public List <Ficha> FichasPlayer1;
        public List <Ficha> FichasPlayer2;
        public Jugador jugador1;
        public Jugador jugador2;
        public Laberinto laberinto;
        public Trampa trampa;
        public PoderFicha poder;
        public int TurnosDeRecarga;
        public GameMazeRunner()
        {
            FichasPlayer1 = new List <Ficha>();
            FichasPlayer2 = new List <Ficha>();
            TurnosDeRecarga = 0;
            StartGame();
        }
        public void StartGame()
        {
            InicializarFichas();
            for (int i = 0; i < 2; i++)
            {
                System.Console.WriteLine("Ingrese su nombre");
                string nombreplayer = Console.ReadLine()??string.Empty;
                if (i == 0)
                    {
                        jugador1 = new Jugador(nombreplayer);
                        MostrarFichas(jugador1);
                    }
                    else 
                    {
                        jugador2 = new Jugador(nombreplayer);
                        MostrarFichas(jugador2);
                    }
            }
            laberinto = new Laberinto(10, 10, jugador1, jugador2);
            trampa = new Trampa(laberinto);
            Play();
        }

        public void Play()
        {
            Jugador jugadoractual;
            while (true)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0) jugadoractual = jugador1;
                    else jugadoractual = jugador2;
                    MovimientoDeFichas(jugadoractual.ficha, jugadoractual);
                }
            }
            
            
        }
                public void InicializarFichas()
                {
                    FichasPlayer1.Add(new Ficha("Sprint", 1, 3, 1));
                    FichasPlayer1.Add(new Ficha("Fortaleza", 1, 4, 2));
                    FichasPlayer1.Add(new Ficha("Reloj de arena", 1, 3, 3));
                    FichasPlayer1.Add(new Ficha("Invisibilidad", 1, 2, 4));
                    FichasPlayer1.Add(new Ficha("Saltadora", 1, 3, 5));
                    FichasPlayer2.Add(new Ficha("Retroceso", 1, 2, 6));
                    FichasPlayer2.Add(new Ficha("Trampero", 1, 5, 7));
                    FichasPlayer2.Add(new Ficha("Fuego", 1, 6, 8));
                    FichasPlayer2.Add(new Ficha("Retraso", 1, 2, 9));
                    FichasPlayer2.Add(new Ficha("Intercambio", 1, 5, 10));
                }
                public void MostrarFichas(Jugador jugador)
                {
                    if (jugador == jugador1)
                    {
                        System.Console.WriteLine("Selecciona la ficha que desee escrbiendo su número correspondiente");
                        System.Console.WriteLine("1 - Nombre : Sprint, Velocidad : 1, Poder : Avanzar 4 casillas, Tiempo de enfriamiento : 3");
                        System.Console.WriteLine("2 - Nombre : Fortaleza, Velocidad : 1, Poder : Avanzar por encima de obstaculos, Tiempo de enfriamiento : 4");
                        System.Console.WriteLine("3 - Nombre : Reloj de arena, Velocidad : 1, Poder : Extender el tiempo de enfriamiento de otra ficha, Tiempo de enfriamiento : 3");
                        System.Console.WriteLine("4 - Nombre : Invisibilidad, Velocidad : 1, Poder : Evita trampas, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("5 - Nombre : Saltadora, Velocidad : 1, Poder : Puede saltar obstaculos y trampas, Tiempo de enfriamiento : 3");
                        SeleccionarFichas (1);
                    }
                    else 
                    {
                        System.Console.WriteLine("Selecciona la ficha que desee escrbiendo su número correspondiente");
                        System.Console.WriteLine("6 - Nombre : Retroceso, Velocidad : 1, Poder : Puede anular su movimiento, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("7 - Nombre : Trampero, Velocidad : 1, Poder : Evita trampas, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("8 - Nombre : Fuego, Velocidad : 1, Poder : Puede destruir obstaculos, Tiempo de enfriamiento : 3");
                        System.Console.WriteLine("9 - Nombre : Retraso, Velocidad : 1, Poder : Hacer retroceder a otro jugador una casilla, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("10 - Nombre : Intercambio, Velocidad : 1, Poder : Permite intercambiar su posicion con la de otra ficha cualquiera, Tiempo de enfriamiento : 5");
                        SeleccionarFichas(2);
                    }
                }
                public void SeleccionarFichas(int jugador)
                {
                    if (jugador == 1)
                    {
                        int ficha1 = int.Parse(Console.ReadLine()??string.Empty);
                        if (ficha1 < 1 || ficha1 > 5)
                        {
                            System.Console.WriteLine("escribe un numero en el rango correspondiente");
                            SeleccionarFichas(1);
                        }
                        else 
                        {
                            System.Console.WriteLine("Has seleccionado la ficha numero " + ficha1 + "!!");
                            jugador1.ficha = FichasPlayer1[ficha1 - 1];
                            System.Console.WriteLine("Presiona una tecla para continuar");
                            Console.ReadKey();
                        }
                    }
                    else if (jugador == 2)
                    {
                        int ficha2 = int.Parse(Console.ReadLine()??string.Empty);
                        if (ficha2 < 6 || ficha2 > 10)
                        {
                            System.Console.WriteLine("escribe un numero en el rango correspondiente");
                            SeleccionarFichas(2);
                        }
                        else 
                        {
                            System.Console.WriteLine("Has seleccionado la ficha numero " + ficha2 + "!!");
                            jugador2.ficha = FichasPlayer2[ficha2 - 6];
                            System.Console.WriteLine("Presiona una tecla para continuar");
                            Console.ReadKey();
                        }
                    }
                }
            public void MovimientoDeFichas(Ficha ficha, Jugador jugadoractual)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.UpArrow && MovimientoValido(ficha.posicion.Item1 - 1, ficha.posicion.Item2))
                {
                    ficha.posicion.Item1--;
                    IncrementarTurno();
                    CaerEnTrampa(jugadoractual);
                }
                else if (tecla.Key == ConsoleKey.DownArrow && MovimientoValido(ficha.posicion.Item1 + 1, ficha.posicion.Item2))
                {
                    ficha.posicion.Item1 ++;
                    IncrementarTurno();
                    CaerEnTrampa(jugadoractual);
                }
                else if (tecla.Key == ConsoleKey.LeftArrow && MovimientoValido(ficha.posicion.Item2 - 1, ficha.posicion.Item2))
                {
                    ficha.posicion.Item2 --;
                    IncrementarTurno();
                    CaerEnTrampa(jugadoractual);
                }
                else if (tecla.Key == ConsoleKey.RightArrow && MovimientoValido(ficha.posicion.Item1 , ficha.posicion.Item2 + 1))
                {
                    ficha.posicion.Item2 ++;
                    IncrementarTurno();
                    CaerEnTrampa(jugadoractual);
                }
                // else if (tecla.Key == ConsoleKey.P)
                // {
                //     UsarPoder(jugador);
                // }
                // else 
                // {
                //     //
                //     System.Console.WriteLine("no te puedes mover hacia esa posición intenta de nuevo");
                //     MovimientoDeFichas(ficha);
                //     //
                // }
                laberinto.ImprimirLab();
            }
        public bool MovimientoValido(int x, int y)
        {
            bool [,] mask = trampa.obstaculos();
            if (mask[x,y]) return true;
            else return false;
        }
        public void IncrementarTurno()
        {
            TurnosDeRecarga ++;
        }
        public bool PuedeUsarPoder(int TurnosDeRecarga, Jugador jugadoractual)
        {
            return TurnosDeRecarga >= jugadoractual.ficha.TiempoDeEnfriamiento;
        }
        public void UsarPoder(Jugador jugadoractual)
        {
            if (PuedeUsarPoder(TurnosDeRecarga, jugadoractual)) 
            {
                switch(jugadoractual.poder.Poderficha)
                {
                    case PoderFicha.Poderes.sprint:
                    Sprint(jugador1.ficha.posicion.Item1, jugador1.ficha.posicion.Item2, jugador1);
                    break;
                    case PoderFicha.Poderes.destroyer:
                    Destroyer(jugador2);
                    break;
                    case PoderFicha.Poderes.fortaleza:
                    Fortaleza(jugador1);
                    break;
                    case PoderFicha.Poderes.reloj:
                    Fortaleza(jugador1);
                    break;
                    case PoderFicha.Poderes.invisibilidad:
                    Invisibilidad(jugador1);
                    break;
                    case PoderFicha.Poderes.retraso:
                    Retraso();
                    break;
                    case PoderFicha.Poderes.retroceso:
                    Retroceso(jugador2);
                    break;
                    case PoderFicha.Poderes.saltar:
                    Saltadora(jugador1);
                    break;
                    case PoderFicha.Poderes.intercambio:
                    Intercambio();
                    break;
                    case PoderFicha.Poderes.trampero:
                    Invisibilidad(jugador2);
                    break;
                }
                TurnosDeRecarga = 0;
            }
        }
        public void CaerEnTrampa(Jugador jugadoractual)
        {
            for (int i = 0; i < trampa.NopuedeUsarPoder().GetLength(0); i ++)
            {
                for (int j = 0; j < trampa.NopuedeUsarPoder().GetLength(1); j ++)
                {
                    if (trampa.NopuedeUsarPoder()[i,j] == 2 && !trampa.SetTrap()[i,j])
                    {
                        jugadoractual.ficha.ColocarFicha(0,0);
                        System.Console.WriteLine("has caido en una trampa");
                    }
                    else if (trampa.NopuedeUsarPoder()[i,j] == 3 && !trampa.SetTrap()[i,j])
                    {
                        if (MovimientoValido(i - 3, j - 2))
                        {
                            jugadoractual.ficha.ColocarFicha(i - 3, j - 2);
                            System.Console.WriteLine("has caido en una trampa");
                        }
                        else
                        {
                            ValPos(i - 3, j - 2, trampa.SetTrap(), jugadoractual);
                            System.Console.WriteLine("has caido en una trampa");
                        }
                    }
                    else if (trampa.NopuedeUsarPoder()[i,j] == 4 && !trampa.SetTrap()[i,j])
                    {
                        jugadoractual.ficha.NopuedeUsarPoder();
                        System.Console.WriteLine("has caido en una trampa");
                    }
                }
            }
        }
        private void ValPos (int i, int j, bool [,] mask, Jugador jugadoractual)
        {
            for (int k = 1; k < mask.GetLength(1); k ++)
            {
                if (mask [k, k]) jugadoractual.ficha.ColocarFicha(k, k);
            }
        }
        public void Sprint(int x, int y, Jugador jugador)
        {
            int [,] distancias = new int [laberinto.Filas, laberinto.Columnas];
            CaminoFast(distancias, x, y, jugador);
            jugador.ficha.TiempoDeEnfriamiento = 3;
        }
        public void Fortaleza (Jugador jugador)
        {
            bool[,] mask = trampa.obstaculos();
            for (int i = 0; i < mask.GetLength(0); i++)
            {
                for (int j = 0; j < mask.GetLength(1); j++)
                {
                    if (!mask[i,j])
                    {
                        if (jugador.ficha.EstaEnPos(i - 1, j) || jugador.ficha.EstaEnPos(i + 1, j) || jugador.ficha.EstaEnPos(i, j - 1) || jugador.ficha.EstaEnPos(i, j + 1))
                        {
                            if (MovimientoValido(i + 1, j)) jugador.ficha.ColocarFicha(i + 1, j);
                            else if (MovimientoValido(i - 1, j)) jugador.ficha.ColocarFicha(i - 1, j);
                            else if (MovimientoValido(i, j - 1)) jugador.ficha.ColocarFicha(i, j - 1);
                            else if (MovimientoValido(i, j + 1)) jugador.ficha.ColocarFicha(i, j + 1); 
                            jugador.ficha.TiempoDeEnfriamiento = 4;
                        }
                    }
                }
            }
        }
        public void Reloj(Jugador jugador)
        {
            switch(jugador.ficha.numero)
            {
                case 5:
                jugador.ficha.TiempoDeEnfriamiento = 6;
                break;
                case 6:
                jugador.ficha.TiempoDeEnfriamiento = 6;
                break;
                case 7:
                jugador.ficha.TiempoDeEnfriamiento = 6;
                break;
                case 8:
                jugador.ficha.TiempoDeEnfriamiento = 6;
                break;
                case 9:
                jugador.ficha.TiempoDeEnfriamiento = 6;
                break;
            }
            jugador.ficha.TiempoDeEnfriamiento = 3;
        }
        public void Invisibilidad(Jugador jugador)
        {
            bool[,] mask = trampa.obstaculos();  
            for (int i = 0; i < mask.GetLength(0); i++)
            {
                for (int j = 0; j < mask.GetLength(1); j++)
                {
                    if (mask[i,j])
                    {
                        if (jugador.ficha.EstaEnPos(i - 1, j) || jugador.ficha.EstaEnPos(i + 1, j) || jugador.ficha.EstaEnPos(i, j - 1) || jugador.ficha.EstaEnPos(i, j + 1))
                        {
                            jugador.ficha.TiempoDeEnfriamiento = 4;
                        }
                    }
                }
            }
        }
        public void Saltadora(Jugador jugador)
        {
            bool[,] mask = trampa.SetTrap();
            for (int i = 0; i < mask.GetLength(0); i++)
            {
                for (int j = 0; j < mask.GetLength(1); j++)
                {
                    if (!mask[i,j])
                    {
                        if (jugador.ficha.EstaEnPos(i - 1, j) || jugador.ficha.EstaEnPos(i + 1, j) || jugador.ficha.EstaEnPos(i, j - 1) || jugador.ficha.EstaEnPos(i, j + 1))
                        {
                            if (MovimientoValido(i + 1, j)) jugador.ficha.ColocarFicha(i + 1, j);
                            else if (MovimientoValido(i - 1, j)) jugador.ficha.ColocarFicha(i - 1, j);
                            else if (MovimientoValido(i, j - 1)) jugador.ficha.ColocarFicha(i, j - 1);
                            else if (MovimientoValido(i, j + 1)) jugador.ficha.ColocarFicha(i, j + 1);
                            jugador.ficha.TiempoDeEnfriamiento = 5;
                        }
                    }
                }
            }
        }
        public void Retroceso(Jugador jugador)
        {
            //Como hacer una pila con esta sola ficha
            jugador.ficha.TiempoDeEnfriamiento = 2;
        }
        public void Destroyer(Jugador jugador)
        {
            bool[,] mask = trampa.obstaculos(); 
            for (int i = 0; i < mask.GetLength(0); i ++)
            {
                for (int j = 0; j < mask.GetLength(1); j ++)
                {
                    if (!mask[i,j])
                    {
                        if (jugador.ficha.EstaEnPos(i - 1, j) || jugador.ficha.EstaEnPos(i + 1, j) || jugador.ficha.EstaEnPos(i, j - 1) || jugador.ficha.EstaEnPos(i, j + 1))
                        {
                            mask[i,j] = true; 
                            jugador.ficha.TiempoDeEnfriamiento = 6;
                        }
                    }
                }
            }
        }
        public void Retraso()
        {
            // //Pasarle como parametro la ficha del player 1
            // if (MovimientoDeFichas(fichaplayer1).Count > 0) 
            // { 
            //     (fichaX, fichaY) = MovimientoDeFichas(fichaplayer1).Pop(); 
            //     Console.WriteLine($"Movimiento deshecho. Ficha regresada a ({fichaX}, {fichaY})");
            //     TiempoDeEnfriamiento = 2; 
            // } 
            // else 
            // { 
            //     Console.WriteLine("No hay movimientos para deshacer."); 
            // }
        }
        public void Intercambio()
        {
            //Intercambiar posiciones entre la ficha del player 1 y la del player 2
        }
        private static bool PosVal(int vf, int vc, int filas, int columnas)
        {
            return vf < filas && vf >= 0 && vc < columnas && vc >= 0;
        }
        private void CaminoFast(int [,] distancia, int x, int y, Jugador jugador)
        {
            bool[,] mask = trampa.obstaculos();
            int [] df = [0, 1, 0, -1];
            int [] dc = [1, 0, -1, 0];
            int [,] distancias = new int [mask.GetLength(0), mask.GetLength(1)];
            distancias[x,y] = 1;
            bool hubocambio;
            do
            {
                hubocambio = false;
                for (int f = 0; f < mask.GetLength(0); f ++)
                {
                    for (int c = 0; c < mask.GetLength(1); c ++)
                    {
                        if (distancias[f,c] == 0) continue;
                        if (!mask[f,c]) continue;
                        for (int k = 0; k < df.Length; k++)
                        {
                            int vf = f + df[k];
                            int vc = c + dc[k];
                            if (PosVal(vf, vc, mask.GetLength(0), mask.GetLength(1)) && distancias[vf, vc] == 0 && mask[vf, vc])
                            {
                                distancias[vf, vc] = distancias[f,c] + 1;
                                if (distancia[vf, vc] - distancia[x,y] == 4)
                                {
                                    jugador.ficha.ColocarFicha(vf, vc);
                                }
                                hubocambio = true;
                                break;
                            }
                        }
                    }
                }
            } while(hubocambio);
        }
        public void EndGame()
        {
            if (jugador1.ficha.posicion == (9,9))
            {
                System.Console.WriteLine("Jugador 1 ha ganado");
            }
            else if(jugador2.ficha.posicion == (9,9))
            {
                System.Console.WriteLine("Jugador 2 ha ganado");
            }
        }
    }
        
