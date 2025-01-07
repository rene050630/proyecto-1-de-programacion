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
            trap = new Trap(Maze);
        }
        public void StartGame()
        {
            InitializeToken();
            for (int i = 0; i < 2; i++)
            {
                System.Console.WriteLine("Ingrese su nombre");
                Label1:
                string nameplayer = Console.ReadLine()??string.Empty;
                if (string.IsNullOrWhiteSpace(nameplayer))
                {
                    System.Console.WriteLine("No puede dejar su nombre en blanco. Escríbalo correctamente");
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
                    System.Console.WriteLine($"nombre : {currentPlayer.token.name}, poder : {currentPlayer.token.TokenPower}, velocidad: {currentPlayer.token.Speed}, tiempo de enfriamineto: {cooldown}");
                    TokenMovement(currentPlayer.token, currentPlayer);
                    if (Player2.token.Number == 8 && !trap.mask[Player2.token.position.Item1, Player2.token.position.Item2])
                    {
                        Maze.maze[Player2.token.position.Item1, Player2.token.position.Item2] = true;
                        trap.maskInt[Player2.token.position.Item1, Player2.token.position.Item2] = 0;
                        trap.mask[Player2.token.position.Item1, Player2.token.position.Item2] = true;
                        trap.maskObs[Player2.token.position.Item1, Player2.token.position.Item2] = true;
                    }
                    if (currentPlayer.token.Number == 4 || currentPlayer.token.Number == 7 && trap.maskInt[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] > 1)
                    {
                        trap.maskInt[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = 0;
                        trap.mask[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = true;
                        trap.maskObs[Player2.token.position.Item1, Player2.token.position.Item2] = true;
                    }
                    RunIntoTraps(currentPlayer);
                    IncreaseShift(currentPlayer);
                    currentPlayer.token.IsActive = false;
                    if (EndGame()) return;
                }
            }  
        }
                public void InitializeToken()
                {
                    TokenPlayer1.Add(new Token("Sprint", 1, 3, Powers.sprint, 1));
                    TokenPlayer1.Add(new Token("Fortress", 1, 4, Powers.fortress, 2));
                    TokenPlayer1.Add(new Token("Clock de arena", 1, 3, Powers.clock, 3));
                    TokenPlayer1.Add(new Token("Ghost", 1, 2, Powers.ghost,4));
                    TokenPlayer1.Add(new Token("Jumper", 1, 3, Powers.jump, 5));
                    TokenPlayer2.Add(new Token("Recoil", 1, 2, Powers.recoil, 6));
                    TokenPlayer2.Add(new Token("Trapper", 1, 5, Powers.trapper, 7));
                    TokenPlayer2.Add(new Token("Fire", 1, 6, Powers.destroyer, 8));
                    TokenPlayer2.Add(new Token("Backwards", 1, 2, Powers.backwards, 9));
                    TokenPlayer2.Add(new Token("Exchange", 1, 5, Powers.exchange, 10));
                }
                public void ShowToken(Player player)
                {
                    if (player == Player1)
                    {
                        System.Console.WriteLine("Selecciona la token que desee escrbiendo su número correspondiente");
                        System.Console.WriteLine("1 - Nombre : Sprint, Velocidad : 1, Poder : Avanzar 4 casillas, Tiempo de enfriamiento : 3");
                        System.Console.WriteLine("2 - Nombre : Fortress, Velocidad : 1, Poder : Avanzar por encima de obstáculos, Tiempo de enfriamiento : 4");
                        System.Console.WriteLine("3 - Nombre : Clock de arena, Velocidad : 1, Poder : Extender el tiempo de enfriamiento de otra ficha, Tiempo de enfriamiento : 3");
                        System.Console.WriteLine("4 - Nombre : Ghost, Velocidad : 1, Poder : Desactiva trampas, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("5 - Nombre : Jumper, Velocidad : 1, Poder : Puede avanzar por encima de obstáculos, Tiempo de enfriamiento : 3");
                        SelectToken (1);
                    }
                    else 
                    {
                        System.Console.WriteLine("Selecciona la token que desee escrbiendo su número correspondiente");
                        System.Console.WriteLine("6 - Nombre : Recoil, Velocidad : 1, Poder : Puede anular su movimiento, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("7 - Nombre : Trapper, Velocidad : 1, Poder : Desactiva traps, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("8 - Nombre : Fire, Velocidad : 1, Poder : Puede destruir obstáculos y trampas, Tiempo de enfriamiento : 3");
                        System.Console.WriteLine("9 - Nombre : Backwards, Velocidad : 1, Poder : Hacer retroceder a otro jugador un movimiento, Tiempo de enfriamiento : 2");
                        System.Console.WriteLine("10 - Nombre : Exchange, Velocidad : 1, Poder : Permite intercambiar su posición con la de otra ficha cualquiera, Tiempo de enfriamiento : 5");
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
                                System.Console.WriteLine("Has seleccionado la ficha número " + token1 + "!!");
                                Player1.token = TokenPlayer1[token1 - 1];
                                System.Console.WriteLine("Presiona una tecla para continuar");
                                Console.ReadKey();
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
                        int token2 = int.Parse(Console.ReadLine()??string.Empty);
                        try
                        {
                            if (token2 < 6 || token2 > 10)
                            {
                                System.Console.WriteLine("escribe un número en el rango correspondiente");
                                SelectToken(2);
                            }
                            else 
                            {
                                System.Console.WriteLine("Has seleccionado la token número " + token2 + "!!");
                                Player2.token = TokenPlayer2[token2 - 6];
                                System.Console.WriteLine("Presiona una tecla para continuar");
                                Console.ReadKey();
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
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                if (tecla.Key == ConsoleKey.P)
                {
                    UsePower(currentPlayer);
                }
                currentPlayer.token.SafePos.Item1 = currentPlayer.token.position.Item1;
                currentPlayer.token.SafePos.Item2 = currentPlayer.token.position.Item2;
                try
                {
                    if (tecla.Key == ConsoleKey.UpArrow && ValidMovement(token.position.Item1 - 1, token.position.Item2, currentPlayer))
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
                        System.Console.WriteLine("has chocado contra un obstáculo");
                        return;
                    }
                }
                catch (Exception)
                {
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
        public void IncreaseShift(Player currentPlayer)
        {
            currentPlayer.token.Recharge ++;
        }
        public bool CanUsePower(Player currentPlayer)
        {
            return currentPlayer.token.Recharge >= currentPlayer.token.Cooldown;
        }
        public void UsePower(Player currentPlayer)
        {
            if (CanUsePower(currentPlayer)) 
            {
                switch(currentPlayer.token.TokenPower)
                {
                    case Powers.sprint:
                    Sprint(Player1.token, Player1);
                    break;
                    case Powers.destroyer:
                    Destroyer(Player2);
                    break;
                    case Powers.fortress:
                    Fortress(Player1);
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
                    Recoil(Player2);
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
                System.Console.WriteLine("has caído en una trampa");
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
                    System.Console.WriteLine("has caído en una trampa");
                    Thread.Sleep(800);
                    Maze.PrintMaze();
                    return;
                }
                else
                {
                    trap.mask[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = true;
                    trap.maskInt[currentPlayer.token.position.Item1, currentPlayer.token.position.Item2] = 0;
                    ValPos(currentPlayer.token.position.Item1 - 1, currentPlayer.token.position.Item2 - 1, trap.mask, currentPlayer);
                    System.Console.WriteLine("has caído en una trampa");
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
                System.Console.WriteLine("has caído en una trampa");
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
        private void ValPos (int h, int k, bool [,] mask, Player currentPlayer)
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
                        currentPlayer.token.SetToken(i,j);
                        return;
                    }
                }
            }
        }
        public void Sprint(Token token, Player currentPlayer)
        {
            for (int i = 0; i < 4; i++)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                try
                {
                    if (tecla.Key == ConsoleKey.UpArrow && ValidMovement(token.position.Item1 - 1, token.position.Item2, currentPlayer))
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
                        System.Console.WriteLine("has chocado contra un obstáculo");
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Se ha ido fuera de los límites del tablero, pierde su turno");
                    Thread.Sleep(800);
                }
                Maze.PrintMaze();
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
        public void Recoil(Player player)
        {
            player.token.position.Item1 = player.token.SafePos.Item1;
            player.token.position.Item2 = player.token.SafePos.Item2;
            Maze.PrintMaze();
            player.token.Cooldown = 2;
        }
        public void Destroyer(Player player)
        {
            player.token.IsActive = true;
            player.token.Cooldown = 6;
        }
        public void Backwards(Player player)
        {
            Player1.token.position.Item1 = Player1.token.SafePos.Item1;
            Player1.token.position.Item2 = Player1.token.SafePos.Item2;
            Maze.PrintMaze();
            player.token.Cooldown = 2;
        }
        public void Exchange(Player player)
        {
            ((Player2.token.position.Item1, Player2.token.position.Item2), (Player1.token.position.Item1, Player1.token.position.Item2)) = ((Player1.token.position.Item1, Player1.token.position.Item2), (Player2.token.position.Item1, Player2.token.position.Item2));
            System.Console.WriteLine("Se han intercambiado las posiciones de los jugadores");
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
                System.Console.WriteLine("Jugador 1 ha ganado!!!!");
                System.Console.WriteLine("Toque una tecla para finalizar el juego");
                Console.ReadKey();
                return true;
            }
            else if(Player2.token.position == (Maze.Rows - 1, Maze.Columns - 1))
            {
                System.Console.WriteLine("Jugador 2 ha ganado");
                System.Console.WriteLine("Toque una tecla para finalizar el juego");
                Console.ReadKey();
                return true;
            }
            return false;
        }
    }
        
