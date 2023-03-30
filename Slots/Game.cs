using Slots.Data;
using System.Drawing;

namespace Slots
{
    public static class Game
    {
        public static void Start()
        {
            while (Statistics.CurrentStats.Score > 0)
            {
                Logic.WinCells = new List<Point>();
                Statistics.CurrentStats.SpinCount++;
                Statistics.CurrentStats.Score -= Settings.Bet;
                if (Statistics.CurrentStats.Score < 0)
                {
                    Statistics.CurrentStats.Score = 0;
                    break;
                }
                Symbol[,] slotBoard = Logic.FillBoard(SlotInfo.Width, SlotInfo.Height);
                double scoreToAdd = Logic.CheckScore(slotBoard);
                Statistics.CurrentStats.TotalLoss += Settings.Bet;
                Statistics.CurrentStats.TotalWin += scoreToAdd;
                Drawing.InitDrawer(slotBoard, scoreToAdd);
                Drawing.DrawEverything();
                GetInput();
                Statistics.CurrentStats.Score += scoreToAdd;
                Thread.Sleep(Settings.SpinSpeed);
                Console.Clear();
            }
            if (Settings.IsAutoplay)
            {
                ConsoleUtils.PrintLine($"Deposited {Settings.DefaultScore} automatically");
                Thread.Sleep(Settings.SpinSpeed * 10);
            }
            else
            {
                ConsoleUtils.PrintLine($"You lost! Deposit some more money?\nAny button to deposit {Settings.DefaultScore}, N to quit.");
                if (Console.ReadKey().Key == ConsoleKey.N)
                {
                    Environment.Exit(0);
                }
            }
            Statistics.CurrentStats.LostCount++;
            Statistics.CurrentStats.Score = Settings.DefaultScore;
            Console.Clear();
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
            bool wasOptionKeyPressed = false;
            switch (pressedKey)
            {
                case ConsoleKey.D:
                    ConsoleUtils.Print("Enter how much do you want to deposit: ");
                    input = ConsoleUtils.GetIntFromInput();
                    if (input != -1)
                    {
                        Statistics.CurrentStats.Score += input;
                        ConsoleUtils.PrintLine($"You deposited {input}!");
                    }
                    wasOptionKeyPressed = true;
                    break;
                case ConsoleKey.B:
                    ConsoleUtils.Print("Enter desired bet: ");
                    input = ConsoleUtils.GetIntFromInput();
                    if (input != -1)
                    {
                        Settings.Bet = input;
                        ConsoleUtils.PrintLine($"Your bet is changed to {input}!");
                    }
                    wasOptionKeyPressed = true;
                    break;
                case ConsoleKey.S:
                    ConsoleUtils.Print("Enter desired spin speed: ");
                    input = ConsoleUtils.GetIntFromInput();
                    if (input != -1)
                    {
                        Settings.SpinSpeed = input;
                        ConsoleUtils.PrintLine($"Spin speed is changed to {input}!");
                    }
                    wasOptionKeyPressed = true;
                    break;
                case ConsoleKey.A:
                    ConsoleUtils.Print("Enter desired number of autospins: ");
                    input = ConsoleUtils.GetIntFromInput();
                    if (input != -1)
                    {
                        if (input == 0)
                        {
                            Settings.IsAutoplay = true;
                            Settings.AutoplaySpins = -1;
                            ConsoleUtils.PrintLine($"Autoplay is turned on for infinite spins!");
                        }
                        else
                        {
                            Settings.IsAutoplay = true;
                            Settings.AutoplaySpins = input;
                            ConsoleUtils.PrintLine($"Autoplay is turned on for {Settings.AutoplaySpins} spins!");
                        }
                        Console.CancelKeyPress += new ConsoleCancelEventHandler(DisableAutoplay);
                    }
                    wasOptionKeyPressed = true;
                    break;
                case ConsoleKey.I:
                    InfoScreen.DrawInfoScreen();
                    Drawing.DrawEverything();
                    GetInput();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
            if (wasOptionKeyPressed && !Settings.IsAutoplay)
            {
                ConsoleUtils.PrintLine("Press any key to spin");
                Console.ReadKey();
            }
            else if (Settings.IsAutoplay)
            {
                if (!(Settings.AutoplaySpins == -1))
                {
                    Settings.AutoplaySpins--;
                }
                ConsoleUtils.PrintLine("Autospin!");
                Thread.Sleep(Settings.SpinSpeed * 5);
                if (Settings.AutoplaySpins == 0)
                {
                    Settings.IsAutoplay = false;
                }
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
