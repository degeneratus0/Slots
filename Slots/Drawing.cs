using Slots.Data;
using System.Drawing;

namespace Slots
{
    public static class Drawing
    {
        public const int SlotWidth = SlotInfo.Width * 4 + 2;
        private static Symbol[,] CurrentSlotBoard = new Symbol[SlotInfo.Width, SlotInfo.Height];
        private static double ScoreToAdd = 0;

        public static void InitDrawer(Symbol[,] SlotBoard, double scoreToAdd)
        {
            CurrentSlotBoard = SlotBoard;
            ScoreToAdd = scoreToAdd;
        }

        public static void DrawEverything()
        {
            Console.CursorVisible = false;
            DrawSlot();
            DrawSymbols();
            DrawMenu();
            Console.CursorVisible = true;
        }

        public static void DrawSlot()
        {
            Console.CursorLeft += ConsoleUtils.TextIndent;
            Console.Write("┌───┬───┬───┬───┬───┐");
            Console.CursorTop += 1;
            Console.CursorLeft -= SlotWidth;
            for (int j = 0; j < SlotInfo.Height; j++)
            {
                Console.CursorLeft += ConsoleUtils.TextIndent;
                Console.Write("│");
                for (int i = 0; i < SlotInfo.Width; i++)
                {
                    Console.Write($"   │");
                }
                Console.CursorTop += 1;
                Console.CursorLeft -= SlotWidth;
            }
            Console.CursorLeft += ConsoleUtils.TextIndent;
            Console.Write("└───┴───┴───┴───┴───┘");
            Console.CursorTop += 1;
            Console.CursorLeft -= SlotWidth;
        }

        public static void DrawSymbols()
        {
            List<Point> winCells = Logic.WinCells;
            for (int i = 0; i < SlotInfo.Width; i++)
            {
                for (int j = 0; j < SlotInfo.Height; j++)
                {
                    Console.SetCursorPosition(i * 4 + 1 + ConsoleUtils.TextIndent, j + 1);
                    Symbol currentSymbol = CurrentSlotBoard[i, j];
                    Console.ForegroundColor = currentSymbol.Color;
                    Console.BackgroundColor = currentSymbol.BackgroundColor;
                    Console.Write($" {CurrentSlotBoard[i, j].Character} ");
                    Console.ResetColor();
                    Thread.Sleep(Settings.SpinSpeed / 5);
                }
                Console.WriteLine();
                Thread.Sleep(Settings.SpinSpeed);
            }
            if (winCells.Any())
            {
                (int X, int Y) cursorPosition = Console.GetCursorPosition();
                DrawWinLines(winCells, CurrentSlotBoard, new Point(cursorPosition.X, cursorPosition.Y));
            }
            Console.WriteLine();
        }

        private static void DrawWinLines(List<Point> winCells, Symbol[,] SlotBoard, Point cursor)
        {
            Dictionary<Point, int> visited = new Dictionary<Point, int>();
            foreach (Point winCell in winCells)
            {
                Console.SetCursorPosition(winCell.X * 4 + 1 + ConsoleUtils.TextIndent, winCell.Y + 1);
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
                Console.ForegroundColor = SlotBoard[winCell.X, winCell.Y].Color;
                Console.Write($" ");
                Thread.Sleep(Settings.SpinSpeed / 10);
                Console.Write($"{SlotBoard[winCell.X, winCell.Y].Character}");
                Thread.Sleep(Settings.SpinSpeed / 10);
                Console.Write($" ");
                Thread.Sleep(Settings.SpinSpeed / 10);
                Console.ResetColor();
            }
            Console.SetCursorPosition(cursor.X, cursor.Y);
        }

        public static void DrawMenu()
        {
            int winLines = Logic.CurrentWinData.Sum(w => w.LineCount);
            if (ScoreToAdd > 0)
            {
                if (ScoreToAdd >= Settings.Bet * Settings.BigWinX)
                {
                    ConsoleUtils.PrintLine("$$$ BIG WIN! $$$");
                    Thread.Sleep(Settings.SpinSpeed * 20);
                    for (double i = 0; i <= ScoreToAdd + 0.01; i += ScoreToAdd / 30)
                    {
                        Math.Round(i, 2);
                        ConsoleUtils.Print($"$$$   {i:f}   $$$");
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Thread.Sleep(Settings.SpinSpeed / 2);
                    }
                    Console.WriteLine();
                }
                ConsoleUtils.PrintLine($"You have won by {winLines} line(s)");
                ConsoleUtils.PrintLine($"Your Score: {Statistics.CurrentStats.Score:f} + {ScoreToAdd:f}!");
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
                ConsoleUtils.PrintLine($"S to change spin speed (current: {Settings.SpinSpeed})");
                ConsoleUtils.PrintLine("A for autoplay");
                ConsoleUtils.PrintLine("I to show info");
                ConsoleUtils.PrintLine("Esc to exit");
            }
            else
            {
                if (Settings.AutoplaySpins == -1)
                {
                    ConsoleUtils.PrintLine($"Autoplay is on!");
                    ConsoleUtils.PrintLine("Ctrl+C to stop");
                }
                else
                {
                    ConsoleUtils.PrintLine("Autoplay is on!");
                    ConsoleUtils.PrintLine($"{Settings.AutoplaySpins} spins left");
                    ConsoleUtils.PrintLine("Ctrl+C to stop");
                }
            }

            Statistics.PrintStat(Statistics.CurrentStats);
        }
    }
}
