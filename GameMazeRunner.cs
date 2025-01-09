public class GameMazeRunner
    {
        public List <Token> TokenPlayer1;
        public List <Token> TokenPlayer2;
        public Player Player1;
        public Player Player2;
        public Maze Maze;
        public Trap trap;
        public GameMazeRunner()
        {
            TokenPlayer1 = new List <Token>();
            TokenPlayer2 = new List <Token>();
            StartGame();
            Player1 = new Player("");
            Player2 = new Player ("");
            Maze = new(10, 10, Player1, Player2);
            Console.Clear();
            trap = new Trap(Maze);
        }
        public void StartGame()
        {
            Console.Clear();
            InitializeToken();
            for (int i = 0; i < 2; i++)
            {
                System.Console.WriteLine("Ingrese su nombre 😀😀😀");
                Label1:
                string nameplayer = Console.ReadLine()??string.Empty;
                if (string.IsNullOrWhiteSpace(nameplayer))
                {
                    System.Console.WriteLine("No puede dejar su nombre en blanco 😔😔😔. Escríbalo correctamente");
                    goto Label1;
                }
                if (i == 0)
                    {
                        Player1 = new Player(nameplayer);
                        ShowToken(Player1);
                    }
                    else 
                    {
                        Player2 = new Player(nameplayer);
                        ShowToken(Player2);
                    }
            }
            Maze = new Maze(10, 10, Player1, Player2);
            trap = new Trap(Maze);
            Play();
        }

        public void Play()
        {
            Player currentPlayer;
            while (true)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0) currentPlayer = Player1;
                    else currentPlayer = Player2;
                    int cooldown = currentPlayer.token.Cooldown - currentPlayer.token.Recharge;
                    if(cooldown <= 0)  cooldown = 0; 
                    System.Console.WriteLine($"NOMBRE: {currentPlayer.token.name}, PODER: {currentPlayer.token.TokenPower}, VELOCIDAD: {currentPlayer.token.Speed}, TIEMPO DE ENFRIAMIENTO: {cooldown}");
                    TokenMovement(currentPlayer.token, currentPlayer);
                    if (Player2.token.Number == 8 && !trap.mask[Player2.token.position.Item1, Player2.token.position.Item2] && Player2.token.IsActive)
                    {
                        Maze.maze[Player2.token.position.Item1, Player2.token.position.Item2] = true;
                        trap.maskInt[Player2.token.position.Item1, Player2.token.position.Item2] = 0;
                        trap.mask[Player2.token.position.Item1, Player2.token.position.Item2] = true;
                        trap.maskObs[Player2.token.position.Item1, Player2.token.position.Item2] = true;
                        System.Console.WriteLine("Has destruido un obstáculo o trampa 🫡🫡🫡");
                        Player2.token.IsActive = false;
                    }
                    if (Player1.token.Number == 4 && trap.maskInt[Player1.token.position.Item1, Player1.token.position.Item2] > 1 && Player1.token.IsActive)
                    {
                        trap.maskInt[Player1.token.position.Item1, Player1.token.position.Item2] = 0;
                        trap.mask[Player1.token.position.Item1, Player1.token.position.Item2] = true;
                        System.Console.WriteLine("Has destruido una trampa 😲😲😲");
                        Player1.token.IsActive = false;
                    }
                    if (Player2.token.Number == 7 && trap.maskInt[Player2.token.position.Item1, Player2.token.position.Item2] > 1 && Player2.token.IsActive)
                    {
                        trap.maskInt[Player2.token.position.Item1, Player2.token.position.Item2] = 0;
                        trap.mask[Player2.token.position.Item1, Player2.token.position.Item2] = true;
                        System.Console.WriteLine("Has destruido una trampa 😲😲😲");
                        Player2.token.IsActive = false;
                    }
                    RunIntoTraps(currentPlayer);
                    IncreaseTurn(currentPlayer);
                    currentPlayer.token.IsActive = false;
                    if (EndGame()) return;
                }
            }  
        }
                public void InitializeToken()
                {
                    TokenPlayer1.Add(new Token("Sprint", 1, 3, Powers.sprint, 1, "👟"));
                    TokenPlayer1.Add(new Token("Backwards", 1, 2, Powers.backwards, 2, "🧙"));
                    TokenPlayer1.Add(new Token("Hourglass", 1, 3, Powers.clock, 3, "⏳"));
                    TokenPlayer1.Add(new Token("Ghost", 1, 2, Powers.ghost, 4, "👻"));
                    TokenPlayer1.Add(new Token("Jumper", 1, 3, Powers.jump, 5, "🦘"));
                    TokenPlayer2.Add(new Token("Clock", 1, 2, Powers.recoil, 6, "🕓"));
                    TokenPlayer2.Add(new Token("Trapper", 1, 5, Powers.trapper, 7, "🕸"));
                    TokenPlayer2.Add(new Token("Fire", 1, 6, Powers.destroyer, 8, "🔥"));
                    TokenPlayer2.Add(new Token("Fortress", 1, 4, Powers.fortress, 9, "🏰"));
                    TokenPlayer2.Add(new Token("Exchange", 1, 5, Powers.exchange, 10, "💱"));
                }
                public void ShowToken(Player player)
                {
                    if (player == Player1)
                    {
                        System.Console.WriteLine("Selecciona la ficha que desee escrbiendo su número correspondiente");
                        System.Console.WriteLine("1 - NOMBRE: Sprint, VELOCIDAD: 1, PODER: Avanzar 4 casillas, TIEMPO DE ENFRIAMIENTO: 3, PERSONAJE: 👟");
                        System.Console.WriteLine("2 - NOMBRE: Backwards, VELOCIDAD: 1, PODER: Hacer retroceder a otro jugador un movimiento, TIEMPO DE ENFRIAMIENTO: 2, PERSONAJE: 🧙‍♂");              
                        System.Console.WriteLine("3 - NOMBRE: Hourglass, VELOCIDAD: 1, PODER: Extender el tiempo de enfriamiento de otra ficha, TIEMPO DE ENFRIAMIENTO: 3, PERSONAJE: ⏳");
                        System.Console.WriteLine("4 - NOMBRE: Ghost, VELOCIDAD: 1, PODER: Desactiva trampas y camina por encima de obstáculos, TIEMPO DE ENFRIAMIENTO: 2, PERSONAJE: 👻");
                        System.Console.WriteLine("5 - NOMBRE: Jumper, VELOCIDAD: 1, PODER: Puede avanzar por encima de obstáculos, TIEMPO DE ENFRIAMIENTO: 3, PERSONAJE: 🦘");
                        SelectToken (1);
                    }
                    else 
                    {
                        Console.Clear();
                        System.Console.WriteLine("Selecciona la ficha que desee escrbiendo su número correspondiente");
                        System.Console.WriteLine("6 - NOMBRE: Clock, VELOCIDAD: 1, PODER: Extender el tiempo de enfriamiento de otra ficha, TIEMPO DE ENFRIAMIENTO: 2, PERSONAJE: 🕓");
                        System.Console.WriteLine("7 - NOMBRE: Trapper, VELOCIDAD: 1, PODER: Desactiva trampas y camina por encima de obstáculos, TIEMPO DE ENFRIAMIENTO: 2, PERSONAJE: 🕸");
                        System.Console.WriteLine("8 - NOMBRE: Fire, VELOCIDAD: 1, PODER: Puede destruir obstáculos y trampas, TIEMPO DE ENFRIAMIENTO: 3, PERSONAJE: 🔥");
                        System.Console.WriteLine("9 - NOMBRE: Fortress, VELOCIDAD: 1, PODER: Avanzar por encima de obstáculos, TIEMPO DE ENFRIAMIENTO: 4, PERSONAJE: 🏰");
                        System.Console.WriteLine("10 - NOMBRE: Exchange, VELOCIDAD: 1, PODER: Permite intercambiar su posición con la de otra ficha cualquiera, TIEMPO DE ENFRIAMIENTO: 5, PERSONAJE:💱");
                        SelectToken(2);
                    }
                }
                public void SelectToken(int player)
                {
                    if (player == 1)
                    {
                        try
                        {
                            int token1 = int.Parse(Console.ReadLine()??string.Empty);
                            if (token1 < 1 || token1 > 5)
                            {
                                System.Console.WriteLine("escribe un número en el rango correspondiente");
                                SelectToken(1);
                            }
                            else 
                            {
                                System.Console.WriteLine("Has seleccionado la ficha número " + token1 + "‼");
                                Player1.token = TokenPlayer1[token1 - 1];
                                System.Console.WriteLine("Presiona una tecla para continuar");
                                Console.ReadKey(true);
                            }
                        }
                        catch(Exception)
                        {
                            System.Console.WriteLine("Escribe un número correcto");
                            SelectToken(1);
                        }
                    }
                    else if (player == 2)
                    {
                        try
                        {
                            int token2 = int.Parse(Console.ReadLine()??string.Empty);
                            if (token2 < 6 || token2 > 10)
                            {
                                System.Console.WriteLine("escribe un número en el rango correspondiente");
                                SelectToken(2);
                            }
                            else 
                            {
                                System.Console.WriteLine("Has seleccionado la ficha número " + token2 + "‼");
                                Player2.token = TokenPlayer2[token2 - 6];
                                System.Console.WriteLine("Presiona una tecla para continuar");
                                Console.ReadKey(true);
                            }
                        }
                        catch (Exception)
                        {
                            System.Console.WriteLine("Escribe un número correcto");
                            SelectToken(2);
                        }
                    }
                }
            public void TokenMovement(Token token, Player currentPlayer)
            {
                if (End()) return;
                try
                {
                    ConsoleKeyInfo tecla = Console.ReadKey(true);
                    currentPlayer.token.SafePos.Item1 = currentPlayer.token.position.Item1;
                    currentPlayer.token.SafePos.Item2 = currentPlayer.token.position.Item2;
                    if (tecla.Key == ConsoleKey.P)
                    {
                        UsePower(currentPlayer);
                    }
                    else if (tecla.Key == ConsoleKey.UpArrow && ValidMovement(token.position.Item1 - 1, token.position.Item2, currentPlayer))
                    {
                        token.position.Item1--;
                    }
                    else if (tecla.Key == ConsoleKey.DownArrow && ValidMovement(token.position.Item1 + 1, token.position.Item2, currentPlayer))
                    {
                        token.position.Item1 ++;
                    }
                    else if (tecla.Key == ConsoleKey.LeftArrow && ValidMovement(token.position.Item1, token.position.Item2 - 1, currentPlayer))
                    {
                        token.position.Item2 --;
                    }
                    else if (tecla.Key == ConsoleKey.RightArrow && ValidMovement(token.position.Item1 , token.position.Item2 + 1, currentPlayer))
                    {
                        token.position.Item2 ++;
                    }
                    else
                    {
                        System.Console.WriteLine("Cuidadoooo!!! ⛔⛔⛔");
                        Thread.Sleep(100);
                        System.Console.WriteLine("Has chocado contra un obstáculo");
                        Thread.Sleep(800);
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Upsss, 😬😬😬");
                    System.Console.WriteLine("Se ha ido fuera de los límites del tablero, pierde su turno");
                    Thread.Sleep(800);
                }
                Maze.PrintMaze();
            }
        public bool ValidMovement(int x, int y, Player currentPlayer)
        {
            bool [,] mask = trap.maskObs;
            if ((mask[x,y] || currentPlayer.token.IsActive) && PosVal(x, y, mask.GetLength(0), mask.GetLength(1))) return true;
            return false;
        }
        public void IncreaseTurn(Player currentPlayer)
        {
            currentPlayer.token.Recharge ++;
        }
        public bool CanUsePower(Player currentPlayer)
        {
            return currentPlayer.token.Recharge >= currentPlayer.token.Cooldown;
        }
        public void UsePower(Player currentPlayer)
        {
            if (End())
                {
                    return;
                }
            if (CanUsePower(currentPlayer)) 
            {
                System.Console.WriteLine(currentPlayer.token.name + ": ha activado el poder 🥵🥵🥵");
                Thread.Sleep(800);
                switch(currentPlayer.token.TokenPower)
                {
                    case Powers.sprint:
                    Sprint(Player1.token, Player1);
                    break;
                    case Powers.destroyer:
                    Destroyer(Player2);
                    break;
                    case Powers.fortress:
                    Fortress(Player2);
                    break;
                    case Powers.clock:
                    Clock();
                    break;
                    case Powers.ghost:
                    Ghost(Player1);
                    break;
                    case Powers.backwards:
                    Backwards(Player2);
                    break;
                    case Powers.recoil:
                    Recoil();
                    break;
                    case Powers.jump:
                    Jumper(Player1);
                    break;
                    case Powers.exchange:
                    Exchange(Player2);
                    break;
                    case Powers.trapper:
                    Ghost(Player2);
                    break;
                }
                currentPlayer.token.Recharge = 0;
                TokenMovement(currentPlayer.token, currentPlayer);
            }
            else
            {
                System.Console.WriteLine("Perdón, 🙁🙁🙁");
                System.Console.WriteLine("No puede usar el poder");
                TokenMovement(currentPlayer.token, currentPlayer);
            }
        }
        public void RunIntoTraps(Player currentPlayer)
        {
            if (trap.maskInt[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] == 2)
            {
                trap.mask[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = true;
                trap.maskInt[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = 0;
                currentPlayer.token.SetToken(0,0);
                System.Console.WriteLine("Has caído en una trampa ☣☣☣");
                Thread.Sleep(600);
                System.Console.WriteLine("Su posición se retrasa hasta el inicio");
                Thread.Sleep(800);
                Maze.PrintMaze();
                return;
            }
            else if (trap.maskInt[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] == 3)
            {
                if (ValidMovement(currentPlayer.token.position.Item1 - 1, currentPlayer.token.position.Item2 - 1, currentPlayer))
                {
                    trap.mask[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = true;
                    trap.maskInt[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = 0;
                    currentPlayer.token.SetToken(currentPlayer.token.position.Item1 - 1, currentPlayer.token.position.Item2 - 1);
                    System.Console.WriteLine("Has caído en una trampa ☣☣☣");
                    Thread.Sleep(600);
                    System.Console.WriteLine("Su posición se retrasa algunas casillas");
                    Thread.Sleep(800);
                    Maze.PrintMaze();
                    return;
                }
                else
                {
                    trap.mask[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = true;
                    trap.maskInt[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = 0;
                    System.Console.WriteLine("Has caído en una trampa ☣☣☣");
                    Thread.Sleep(600);
                    if (ValPos(currentPlayer.token.position.Item1 - 1, currentPlayer.token.position.Item2 - 1, trap.mask, currentPlayer))
                    System.Console.WriteLine("Su posición se retrasa algunas casillas");
                    else 
                    System.Console.Write(" pero no hay movimientos posibles, tuviste suerte");
                    Thread.Sleep(800);
                    Maze.PrintMaze();
                    return;
                }
            }
            else if (trap.maskInt[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] == 4)
            {
                trap.mask[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = true;
                trap.maskInt[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = 0;
                CannotUsePower(currentPlayer);
                System.Console.WriteLine("Has caído en una trampa ☣☣☣");
                Thread.Sleep(600);
                System.Console.WriteLine("Su Tiempo de enfriamiento se ha incrementado");
                Thread.Sleep(800);
                Maze.PrintMaze();
                return;
            }
        }
        public void CannotUsePower(Player currentPlayer)
        {
            currentPlayer.token.Cooldown = 6;
            currentPlayer.token.Recharge = 0;
        }
        private bool ValPos (int h, int k, bool [,] mask, Player currentPlayer)
        {
            for (int i = 1; i < h; i++)
            {
                if (i == h - 1)
                {
                    return false;
                }
                for (int j = 1; j < k; j++)
                {
                    if (mask[i,j]) 
                    {
                        currentPlayer.token.SetToken(i,j);
                        return true;
                    }
                }
            }
            return false;
        }
        public void Sprint(Token token, Player currentPlayer)
        {
            System.Console.WriteLine("Puedes avanzar 5 pasos 🤗🤗🤗");
            for (int i = 0; i < 4; i++)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                try
                {
                    if (tecla.Key == ConsoleKey.UpArrow && ValidMovement(token.position.Item1 - 1, token.position.Item2, currentPlayer))
                    {
                        token.position.Item1--;
                        Maze.PrintMaze();
                    }
                    else if (tecla.Key == ConsoleKey.DownArrow && ValidMovement(token.position.Item1 + 1, token.position.Item2, currentPlayer))
                    {
                        token.position.Item1 ++;
                        Maze.PrintMaze();
                    }
                    else if (tecla.Key == ConsoleKey.LeftArrow && ValidMovement(token.position.Item1, token.position.Item2 - 1, currentPlayer))
                    {
                        token.position.Item2 --;
                        Maze.PrintMaze();
                    }
                    else if (tecla.Key == ConsoleKey.RightArrow && ValidMovement(token.position.Item1 , token.position.Item2 + 1, currentPlayer))
                    {
                        token.position.Item2 ++;  
                        Maze.PrintMaze();                  
                    }
                    else 
                    {
                        System.Console.WriteLine("Cuidadoooo!!! ⛔⛔⛔");
                        Thread.Sleep(100);
                        System.Console.WriteLine("Has chocado contra un obstáculo");
                        Thread.Sleep(800);
                    }
                    if (End())
                    {
                        Maze.PrintMaze();
                        return;
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Upsss, 😬😬😬");
                    System.Console.WriteLine("Se ha ido fuera de los límites del tablero");
                    Thread.Sleep(800);
                }
            }
            currentPlayer.token.Cooldown = 3;
        }
        public void Fortress (Player player)
        {
            player.token.IsActive = true;
            player.token.Cooldown = 4;
        }
        public void Clock()
        {
            Player2.token.Cooldown = 6;
            Player2.token.Recharge = 0;
            Player1.token.Cooldown = 3; 
            System.Console.WriteLine("Se ha aumentado el tiempo de enfriamiento de la otra ficha 😉😉😉");
        }
        public void Recoil()
        {
            Player1.token.Cooldown = 6;
            Player1.token.Recharge = 0;
            Player2.token.Cooldown = 3; 
            System.Console.WriteLine("Se ha aumentado el tiempo de enfriamiento de la otra ficha 😉😉😉");
        }
        public void Ghost(Player player)
        {
            player.token.IsActive = true;
            player.token.Cooldown = 4;
        }
        public void Jumper(Player player)
        {
            player.token.IsActive = true;
            player.token.Cooldown = 5;
        }
        public void Destroyer(Player player)
        {
            player.token.IsActive = true;
            player.token.Cooldown = 6;
        }
        public void Backwards(Player player)
        {
            Player2.token.position.Item1 = Player2.token.SafePos.Item1;
            Player2.token.position.Item2 = Player2.token.SafePos.Item2;
            System.Console.WriteLine("Se ha anulado el movimiento de la otra ficha 😄😄😄");
            Thread.Sleep(800);
            Maze.PrintMaze();
            player.token.Cooldown = 2;
        }
        public void Exchange(Player player)
        {
            ((Player2.token.position.Item1, Player2.token.position.Item2), (Player1.token.position.Item1, Player1.token.position.Item2)) = ((Player1.token.position.Item1, Player1.token.position.Item2), (Player2.token.position.Item1, Player2.token.position.Item2));
            System.Console.WriteLine("Se han intercambiado las posiciones de los jugadores 🧐🧐🧐");
            Thread.Sleep(800);
            Maze.PrintMaze();
            player.token.Cooldown = 5;
            return; 
        }
        private static bool PosVal(int vf, int vc, int filas, int columnas)
        {
            return vf < filas && vf >= 0 && vc < columnas && vc >= 0;
        }
        public bool EndGame()
        {
            if (Player1.token.position == (Maze.Rows - 1, Maze.Columns - 1))
            {
                System.Console.WriteLine("Enhorabuena, Jugador 1 ha ganado!!!! 🥳🥳🥳");
                System.Console.WriteLine("Toque una tecla para finalizar el juego");
                Console.ReadKey(true);
                return true;
            }
            else if(Player2.token.position == (Maze.Rows - 1, Maze.Columns - 1))
            {
                System.Console.WriteLine("Enhorabuena, Jugador 2 ha ganado!!!! 🥳🥳🥳");
                System.Console.WriteLine("Toque una tecla para finalizar el juego");
                Console.ReadKey(true);
                return true;
            }
            return false;
        }
        public bool End()
        {
            if (Player1.token.position == (Maze.Rows - 1, Maze.Columns - 1))
            {
                return true;
            }
            else if(Player2.token.position == (Maze.Rows - 1, Maze.Columns - 1))
            {
                return true;
            }
            return false;
        }
    }
        
