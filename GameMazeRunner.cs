public class GameMazeRunner
    {
        public List <Ficha> FichasPlayer1;
        public List <Ficha> FichasPlayer2;
        public Jugador jugador1;
        public Jugador jugador2;
        public Laberinto laberinto;
        public Trampa trampa;
        public GameMazeRunner()
        {
            FichasPlayer1 = new List <Ficha>();
            FichasPlayer2 = new List <Ficha>();
            StartGame();
            jugador1 = new Jugador("");
            jugador2 = new Jugador ("");
            laberinto = new(10, 10, jugador1, jugador2);
            trampa = new Trampa(laberinto);
        }
        public void StartGame()
        {
            InicializarFichas();
            for (int i = 0; i < 2; i++)
            {
                System.Console.WriteLine("Ingrese su nombre");
                Label1:
                string nombreplayer = Console.ReadLine()??string.Empty;
                if (string.IsNullOrWhiteSpace(nombreplayer))
                {
                    System.Console.WriteLine("No puede dejar su nombre en blanco. Escríbalo correctamente");
                    goto Label1;
                }
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
                    int cooldown = jugadoractual.ficha.TiempoDeEnfriamiento - jugadoractual.ficha.TurnosDeRecarga;
                    if(cooldown <= 0)  cooldown = 0; 
                    System.Console.WriteLine($"nombre : {jugadoractual.ficha.nombre}, poder : {jugadoractual.ficha.Poderficha}, su velocidad es: {jugadoractual.ficha.velocidad}, tiempo de enfriamineto : {cooldown}");
                    if (EndGame()) break;
                    MovimientoDeFichas(jugadoractual.ficha, jugadoractual);
                    if (jugador2.ficha.numero == 8 && !trampa.mask[jugador2.ficha.posicion.Item1, jugador2.ficha.posicion.Item2])
                    {
                        laberinto.laberinto[jugador2.ficha.posicion.Item1, jugador2.ficha.posicion.Item2] = true;
                        trampa.maskInt[jugador2.ficha.posicion.Item1, jugador2.ficha.posicion.Item2] = 0;
                        trampa.mask[jugador2.ficha.posicion.Item1, jugador2.ficha.posicion.Item2] = true;
                    }
                    if (jugadoractual.ficha.numero == 4 || jugadoractual.ficha.numero == 7 && trampa.maskInt[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] > 1)
                    {
                        trampa.maskInt[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] = 0;
                    }
                    CaerEnTrampa(jugadoractual);
                    IncrementarTurno(jugadoractual);
                    jugadoractual.ficha.IsActive = false;
                }
                if (EndGame()) break;
            }  
        }
                public void InicializarFichas()
                {
                    FichasPlayer1.Add(new Ficha("Sprint", 1, 3, Poderes.sprint, 1));
                    FichasPlayer1.Add(new Ficha("Fortaleza", 1, 4, Poderes.fortaleza, 2));
                    FichasPlayer1.Add(new Ficha("Reloj de arena", 1, 3, Poderes.reloj, 3));
                    FichasPlayer1.Add(new Ficha("Invisibilidad", 1, 2, Poderes.invisibilidad,4));
                    FichasPlayer1.Add(new Ficha("Saltadora", 1, 3, Poderes.saltar, 5));
                    FichasPlayer2.Add(new Ficha("Retroceso", 1, 2, Poderes.retroceso, 6));
                    FichasPlayer2.Add(new Ficha("Trampero", 1, 5, Poderes.trampero, 7));
                    FichasPlayer2.Add(new Ficha("Fuego", 1, 6, Poderes.destroyer, 8));
                    FichasPlayer2.Add(new Ficha("Retraso", 1, 2, Poderes.retraso, 9));
                    FichasPlayer2.Add(new Ficha("Intercambio", 1, 5, Poderes.intercambio, 10));
                }
                public void MostrarFichas(Jugador jugador)
                {
                    if (jugador == jugador1)
                    {
                        System.Console.WriteLine("Selecciona la ficha que desee escrbiendo su número correspondiente");
                        System.Console.WriteLine("1 - Nombre : Sprint, Velocidad : 1, Poder : Avanzar 4 casillas, Tiempo de enfriamiento : 3");
                        System.Console.WriteLine("2 - Nombre : Fortaleza, Velocidad : 1, Poder : Avanzar por encima de obstáculos, Tiempo de enfriamiento : 4");
                        System.Console.WriteLine("3 - Nombre : Reloj de arena, Velocidad : 1, Poder : Extender el tiempo de enfriamiento de otra ficha, Tiempo de enfriamiento : 3");
                        System.Console.WriteLine("4 - Nombre : Invisibilidad, Velocidad : 1, Poder : Desactiva trampas, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("5 - Nombre : Saltadora, Velocidad : 1, Poder : Puede avanzar por encima de obstáculos, Tiempo de enfriamiento : 3");
                        SeleccionarFichas (1);
                    }
                    else 
                    {
                        System.Console.WriteLine("Selecciona la ficha que desee escrbiendo su número correspondiente");
                        System.Console.WriteLine("6 - Nombre : Retroceso, Velocidad : 1, Poder : Puede anular su movimiento, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("7 - Nombre : Trampero, Velocidad : 1, Poder : Desactiva trampas, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("8 - Nombre : Fuego, Velocidad : 1, Poder : Puede destruir obstaculos y trampas, Tiempo de enfriamiento : 3");
                        System.Console.WriteLine("9 - Nombre : Retraso, Velocidad : 1, Poder : Hacer retroceder a otro jugador un movimiento, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("10 - Nombre : Intercambio, Velocidad : 1, Poder : Permite intercambiar su posicion con la de otra ficha cualquiera, Tiempo de enfriamiento : 5");
                        SeleccionarFichas(2);
                    }
                }
                public void SeleccionarFichas(int jugador)
                {
                    if (jugador == 1)
                    {
                        try
                        {
                            int ficha1 = int.Parse(Console.ReadLine()??string.Empty);
                            if (ficha1 < 1 || ficha1 > 5)
                            {
                                System.Console.WriteLine("escribe un número en el rango correspondiente");
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
                        catch(Exception)
                        {
                            System.Console.WriteLine("Escribe un número correcto");
                            SeleccionarFichas(1);
                        }
                    }
                    else if (jugador == 2)
                    {
                        int ficha2 = int.Parse(Console.ReadLine()??string.Empty);
                        try
                        {
                            if (ficha2 < 6 || ficha2 > 10)
                            {
                                System.Console.WriteLine("escribe un número en el rango correspondiente");
                                SeleccionarFichas(2);
                            }
                            else 
                            {
                                System.Console.WriteLine("Has seleccionado la ficha número " + ficha2 + "!!");
                                jugador2.ficha = FichasPlayer2[ficha2 - 6];
                                System.Console.WriteLine("Presiona una tecla para continuar");
                                Console.ReadKey();
                            }
                        }
                        catch (Exception)
                        {
                            System.Console.WriteLine("Escribe un número correcto");
                            SeleccionarFichas(2);
                        }
                    }
                }
            public void MovimientoDeFichas(Ficha ficha, Jugador jugadoractual)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.P)
                {
                    UsarPoder(jugadoractual);
                }
                jugadoractual.ficha.SafePos.Item1 = jugadoractual.ficha.posicion.Item1;
                jugadoractual.ficha.SafePos.Item2 = jugadoractual.ficha.posicion.Item2;
                try
                {
                    if (tecla.Key == ConsoleKey.UpArrow && MovimientoValido(ficha.posicion.Item1 - 1, ficha.posicion.Item2, jugadoractual))
                    {
                        ficha.posicion.Item1--;
                    }
                    else if (tecla.Key == ConsoleKey.DownArrow && MovimientoValido(ficha.posicion.Item1 + 1, ficha.posicion.Item2, jugadoractual))
                    {
                        ficha.posicion.Item1 ++;
                    }
                    else if (tecla.Key == ConsoleKey.LeftArrow && MovimientoValido(ficha.posicion.Item1, ficha.posicion.Item2 - 1, jugadoractual))
                    {
                        ficha.posicion.Item2 --;
                    }
                    else if (tecla.Key == ConsoleKey.RightArrow && MovimientoValido(ficha.posicion.Item1 , ficha.posicion.Item2 + 1, jugadoractual))
                    {
                        ficha.posicion.Item2 ++;
                    }
                    else 
                    {
                        System.Console.WriteLine("has chocado contra un obstáculo");
                        return;
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Se ha ido fuera de los límites del tablero, pierde su turno");
                    Thread.Sleep(800);
                }
                laberinto.ImprimirLab();
            }
        public bool MovimientoValido(int x, int y, Jugador jugadoractual)
        {
            bool [,] mask = trampa.obstaculos();
            if ((mask[x,y] || jugadoractual.ficha.IsActive) && PosVal(x, y, mask.GetLength(0), mask.GetLength(1))) return true;
            return false;
        }
        public void IncrementarTurno(Jugador jugadoractual)
        {
            jugadoractual.ficha.TurnosDeRecarga ++;
        }
        public bool PuedeUsarPoder(Jugador jugadoractual)
        {
            return jugadoractual.ficha.TurnosDeRecarga >= jugadoractual.ficha.TiempoDeEnfriamiento;
        }
        public void UsarPoder(Jugador jugadoractual)
        {
            if (PuedeUsarPoder(jugadoractual)) 
            {
                switch(jugadoractual.ficha.Poderficha)
                {
                    case Poderes.sprint:
                    Sprint(jugador1.ficha, jugador1);
                    break;
                    case Poderes.destroyer:
                    Destroyer(jugador2);
                    break;
                    case Poderes.fortaleza:
                    Fortaleza(jugador1);
                    break;
                    case Poderes.reloj:
                    Reloj();
                    break;
                    case Poderes.invisibilidad:
                    Invisibilidad(jugador1);
                    break;
                    case Poderes.retraso:
                    Retraso(jugador2);
                    break;
                    case Poderes.retroceso:
                    Retroceso(jugador2);
                    break;
                    case Poderes.saltar:
                    Saltadora(jugador1);
                    break;
                    case Poderes.intercambio:
                    Intercambio(jugador2);
                    break;
                    case Poderes.trampero:
                    Invisibilidad(jugador2);
                    break;
                }
                jugadoractual.ficha.TurnosDeRecarga = 0;
                MovimientoDeFichas(jugadoractual.ficha, jugadoractual);
            }
            else
            {
                System.Console.WriteLine("No puede usar el poder");
                MovimientoDeFichas(jugadoractual.ficha, jugadoractual);
            }
        }
        public void CaerEnTrampa(Jugador jugadoractual)
        {
            if (trampa.maskInt[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] == 2)
            {
                trampa.mask[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] = true;
                trampa.maskInt[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] = 0;
                jugadoractual.ficha.ColocarFicha(0,0);
                System.Console.WriteLine("has caído en una trampa");
                laberinto.ImprimirLab();
                Thread.Sleep(800);
                return;
            }
            else if (trampa.maskInt[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] == 3)
            {
                if (MovimientoValido(jugadoractual.ficha.posicion.Item1 - 1, jugadoractual.ficha.posicion.Item2 - 1, jugadoractual))
                {
                    trampa.mask[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] = true;
                    trampa.maskInt[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] = 0;
                    jugadoractual.ficha.ColocarFicha(jugadoractual.ficha.posicion.Item1 - 1, jugadoractual.ficha.posicion.Item2 - 1);
                    System.Console.WriteLine("has caído en una trampa");
                    laberinto.ImprimirLab();
                    Thread.Sleep(800);
                    return;
                }
                else
                {
                    trampa.mask[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] = true;
                    trampa.maskInt[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] = 0;
                    ValPos(jugadoractual.ficha.posicion.Item1 - 1, jugadoractual.ficha.posicion.Item2 - 1, trampa.mask, jugadoractual);
                    System.Console.WriteLine("has caído en una trampa");
                    laberinto.ImprimirLab();
                    Thread.Sleep(800);
                    return;
                }
            }
            else if (trampa.maskInt[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] == 4)
            {
                trampa.mask[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] = true;
                trampa.maskInt[jugadoractual.ficha.posicion.Item1, jugadoractual.ficha.posicion.Item2] = 0;
                NopuedeUsarPoder(jugadoractual);
                System.Console.WriteLine("has caído en una trampa");
                laberinto.ImprimirLab();
                Thread.Sleep(800);
                return;
            }
        }
        public void NopuedeUsarPoder(Jugador jugadoractual)
        {
            jugadoractual.ficha.TurnosDeRecarga = 0;
        }
        private void ValPos (int h, int k, bool [,] mask, Jugador jugadoractual)
        {
            for (int i = 1; i < h; i++)
            {
                if (i == h - 1)
                {
                    System.Console.WriteLine("No hay movimientos posibles");
                    return;
                }
                for (int j = 1; j < k; j++)
                {
                    if (mask[i,j]) 
                    {
                        jugadoractual.ficha.ColocarFicha(i,j);
                        return;
                    }
                }
            }
        }
        public void Sprint(Ficha ficha, Jugador jugadoractual)
        {
            for (int i = 0; i < 4; i++)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                try
                {
                    if (tecla.Key == ConsoleKey.UpArrow && MovimientoValido(ficha.posicion.Item1 - 1, ficha.posicion.Item2, jugadoractual))
                    {
                        ficha.posicion.Item1--;
                    }
                    else if (tecla.Key == ConsoleKey.DownArrow && MovimientoValido(ficha.posicion.Item1 + 1, ficha.posicion.Item2, jugadoractual))
                    {
                        ficha.posicion.Item1 ++;
                    }
                    else if (tecla.Key == ConsoleKey.LeftArrow && MovimientoValido(ficha.posicion.Item1, ficha.posicion.Item2 - 1, jugadoractual))
                    {
                        ficha.posicion.Item2 --;
                    }
                    else if (tecla.Key == ConsoleKey.RightArrow && MovimientoValido(ficha.posicion.Item1 , ficha.posicion.Item2 + 1, jugadoractual))
                    {
                        ficha.posicion.Item2 ++;                    
                    }
                    else 
                    {
                        System.Console.WriteLine("has chocado contra un obstáculo");
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Se ha ido fuera de los límites del tablero, pierde su turno");
                    Thread.Sleep(800);
                }
                laberinto.ImprimirLab();
            }
            jugadoractual.ficha.TiempoDeEnfriamiento = 3;
        }
        public void Fortaleza (Jugador jugador)
        {
            jugador.ficha.IsActive = true;
            jugador.ficha.TiempoDeEnfriamiento = 4;
        }
        public void Reloj()
        {
            jugador2.ficha.TiempoDeEnfriamiento = 6;
            jugador2.ficha.TurnosDeRecarga = 0;
            jugador1.ficha.TiempoDeEnfriamiento = 3; 
        }
        public void Invisibilidad(Jugador jugador)
        {
            jugador.ficha.IsActive = true;
            jugador.ficha.TiempoDeEnfriamiento = 4;
                        
        }
        public void Saltadora(Jugador jugador)
        {
            jugador.ficha.IsActive = true;
            jugador.ficha.TiempoDeEnfriamiento = 5;
        }
        public void Retroceso(Jugador jugador)
        {
            jugador.ficha.posicion.Item1 = jugador.ficha.SafePos.Item1;
            jugador.ficha.posicion.Item2 = jugador.ficha.SafePos.Item2;
            laberinto.ImprimirLab();
            jugador.ficha.TiempoDeEnfriamiento = 2;
        }
        public void Destroyer(Jugador jugador)
        {
            jugador.ficha.IsActive = true;
            jugador.ficha.TiempoDeEnfriamiento = 6;
        }
        public void Retraso(Jugador jugador)
        {
            jugador1.ficha.posicion.Item1 = jugador1.ficha.SafePos.Item1;
            jugador1.ficha.posicion.Item2 = jugador1.ficha.SafePos.Item2;
            laberinto.ImprimirLab();
            jugador.ficha.TiempoDeEnfriamiento = 2;
        }
        public void Intercambio(Jugador jugador)
        {
            ((jugador2.ficha.posicion.Item1, jugador2.ficha.posicion.Item2), (jugador1.ficha.posicion.Item1, jugador1.ficha.posicion.Item2)) = ((jugador1.ficha.posicion.Item1, jugador1.ficha.posicion.Item2), (jugador2.ficha.posicion.Item1, jugador2.ficha.posicion.Item2));
            laberinto.ImprimirLab();
            jugador.ficha.TiempoDeEnfriamiento = 5;
            System.Console.WriteLine("Se han intercambiado las posiciones de los jugadores");
            return; 
        }
        private static bool PosVal(int vf, int vc, int filas, int columnas)
        {
            return vf < filas && vf >= 0 && vc < columnas && vc >= 0;
        }
        public bool EndGame()
        {
            if (jugador1.ficha.posicion == (laberinto.Filas - 1, laberinto.Columnas - 1))
            {
                System.Console.WriteLine("Jugador 1 ha ganado");
                return true;
            }
            else if(jugador2.ficha.posicion == (laberinto.Filas - 1, laberinto.Columnas - 1))
            {
                System.Console.WriteLine("Jugador 2 ha ganado");
                return true;
            }
            return false;
        }
    }
        
