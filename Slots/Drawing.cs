using Slots.Data;

namespace Slots
{
    public static class Drawing
    {
        private static int SlotIndent = 1;

        public static void DrawSlot()
        {
            Console.CursorVisible = false;
            Console.CursorLeft += SlotIndent;
            Console.WriteLine("┌───┬───┬───┬───┬───┐");
            for (int j = 0; j < Settings.m; j++)
            {
                Console.CursorLeft += SlotIndent;
                Console.Write("│");
                for (int i = 0; i < Settings.n; i++)
                {
                    Console.Write($"   │");
                }
                Console.WriteLine();
            }
            Console.CursorLeft += SlotIndent;
            Console.WriteLine("└───┴───┴───┴───┴───┘");
        }

        public static void DrawSymbols(List<(int, int)> winCells, Symbol[,] slotBoard)
        {
            for (int i = 0; i < Settings.n; i++)
            {
                for (int j = 0; j < Settings.m; j++)
                {
                    Console.SetCursorPosition(i * 4 + 1 + SlotIndent, j + 1);
                    Symbol currentSymbol = slotBoard[i, j];
                    Console.ForegroundColor = currentSymbol.Color;
                    Console.BackgroundColor = currentSymbol.BackgroundColor;
                    Console.Write($" {slotBoard[i, j].Character} ");
                    Console.ResetColor();
                    Thread.Sleep(Settings.SpinSpeed / 5);
                }
                Console.WriteLine();
                Thread.Sleep(Settings.SpinSpeed);
            }
            if (winCells.Any())
            {
                DrawWinLines(winCells, slotBoard, Console.GetCursorPosition());
            }
            Console.WriteLine();
        }

        private static void DrawWinLines(List<(int, int)> winCells, Symbol[,] slotBoard, (int left, int top) cursor)
        {
            Dictionary<(int, int), int> visited = new Dictionary<(int, int), int>();
            foreach ((int i, int j) winCell in winCells)
            {
                Console.SetCursorPosition(winCell.i * 4 + 1 + SlotIndent, winCell.j + 1);
                if (!visited.ContainsKey(winCell))
                {
                    visited.Add(winCell, 1);
                    Console.BackgroundColor = ConsoleColor.Magenta;
                }
                else if (visited[winCell] > 2)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                }
                else if (visited[winCell] == 2)
                {
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                }
                visited[winCell]++;
                Console.ForegroundColor = slotBoard[winCell.i, winCell.j].Color;
                Console.Write($" {slotBoard[winCell.i, winCell.j].Character} ");
                Console.ResetColor();
                Thread.Sleep(Settings.SpinSpeed);
            }
            Console.SetCursorPosition(cursor.left, cursor.top);
        }

        public static void DrawMenu(double scoreToAdd, double winLines)
        {
            if (scoreToAdd > 0)
            {
                if (scoreToAdd >= Settings.Bet * Settings.BigWinX)
                {
                    Console.WriteLine("$$$ BIG WIN! $$$");
                    Thread.Sleep(Settings.SpinSpeed * 20);
                    for (double i = 0; i <= scoreToAdd + 0.01; i += scoreToAdd / 30)
                    {
                        Math.Round(i, 2);
                        Console.Write($"$$$   {i:f}   $$$");
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Thread.Sleep(Settings.SpinSpeed / 2);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"You have won by {winLines} line(s)");
                Console.WriteLine($"Your Score: {Statistics.CurrentStats.Score:f} + {scoreToAdd:f}!");
            }
            else
            {
                ConsoleUtils.PrintLine($"Your Score: {Statistics.CurrentStats.Score:f}");
            }
            ConsoleUtils.PrintLine($"Your Bet: {Settings.Bet}");
            Console.WriteLine();
            if (!Settings.IsAutoplay)
            {
                ConsoleUtils.PrintLine("Any key to spin");
                ConsoleUtils.PrintLine("D to deposit");
                ConsoleUtils.PrintLine("B to change bet");
                ConsoleUtils.PrintLine("S to change spin speed");
                ConsoleUtils.PrintLine("A for autoplay");
            }
            else
            {
                if (Settings.AutoplaySpins == -1)
                {
                    Console.WriteLine($"Autoplay is on!\nCtrl+C to stop");
                }
                else
                {
                    Console.WriteLine($"Autoplay is on!\n{Settings.AutoplaySpins} spins left\nCtrl+C to stop");
                }
            }

            Statistics.PrintStat(Statistics.CurrentStats);

            GetInput();
        }

        private static void GetInput()
        {
            Console.CursorVisible = true;
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
            ConsoleKey pressedKey = ConsoleKey.NoName;
            if (!Settings.IsAutoplay)
            {
                pressedKey = Console.ReadKey(true).Key;
            }
            int input;
            bool wasAnyOptionButtonPressed = false;
            switch (pressedKey)
            {
                case ConsoleKey.D:
                    Console.Write("Enter how much do you want to deposit: ");
                    input = GetIntFromInput();
                    if (input != -1)
                    {
                        Logic.AddScore(input);
                        Console.WriteLine($"You deposited {input}!");
                    }
                    wasAnyOptionButtonPressed = true;
                    break;
                case ConsoleKey.B:
                    Console.Write("Enter desired bet: ");
                    input = GetIntFromInput();
                    if (input != -1)
                    {
                        Settings.Bet = input;
                        Console.WriteLine($"Your bet is changed to {input}!");
                    }
                    wasAnyOptionButtonPressed = true;
                    break;
                case ConsoleKey.S:
                    Console.Write("Enter desired spin speed: ");
                    input = GetIntFromInput();
                    if (input != -1)
                    {
                        Settings.SpinSpeed = input;
                        Console.WriteLine($"Spin speed is changed to {input}!");
                    }
                    wasAnyOptionButtonPressed = true;
                    break;
                case ConsoleKey.A:
                    Console.Write("Enter desired number of autospins: ");
                    input = GetIntFromInput();
                    if (input != -1)
                    {
                        if (input == 0)
                        {
                            Settings.IsAutoplay = true;
                            Settings.AutoplaySpins = -1;
                            Console.WriteLine($"Autoplay is turned on for infinite spins!");
                        }
                        else
                        {
                            Settings.IsAutoplay = true;
                            Settings.AutoplaySpins = input;
                            Console.WriteLine($"Autoplay is turned on for {Settings.AutoplaySpins} spins!");
                        }
                        Console.CancelKeyPress += new ConsoleCancelEventHandler(DisableAutoplay);
                    }
                    wasAnyOptionButtonPressed = true;
                    break;
            }
            if (wasAnyOptionButtonPressed && !Settings.IsAutoplay)
            {
                Console.WriteLine("Press any key to spin");
                Console.ReadKey();
            }
            else if (Settings.IsAutoplay)
            {
                if (!(Settings.AutoplaySpins == -1))
                {
                    Settings.AutoplaySpins--;
                }
                Console.WriteLine("Autospin!");
                Thread.Sleep(Settings.SpinSpeed * 5);
                if (Settings.AutoplaySpins == 0)
                {
                    Settings.IsAutoplay = false;
                }
            }
        }

        private static int GetIntFromInput()
        {
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong Format");
                return -1;
            }
        }

        private static void DisableAutoplay(object sender, ConsoleCancelEventArgs args)
        {
            Settings.IsAutoplay = false;
            args.Cancel = true;
            Console.CancelKeyPress -= new ConsoleCancelEventHandler(DisableAutoplay);
        }
    }
}
