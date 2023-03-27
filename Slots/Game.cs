using Slots.Data;

namespace Slots
{
    public static class Game
    {

        public static void Start()
        {
            while (Statistics.CurrentStats.Score > 0)
            {
                Logic.WinCells = new List<(int, int)>();
                Symbol[,] SlotBoard = Logic.FillBoard(Settings.n, Settings.m);
                double ScoreToAdd = Logic.CheckScore(SlotBoard);
                Drawing.DrawSlot();
                Drawing.DrawSymbols(Logic.WinCells, SlotBoard);
                Statistics.CurrentStats.SpinCount++;
                Statistics.CurrentStats.Score -= Settings.Bet;
                if (Statistics.CurrentStats.Score < 0)
                {
                    Statistics.CurrentStats.Score = 0;
                }
                Statistics.CurrentStats.TotalLoss += Settings.Bet;
                Statistics.CurrentStats.TotalWin += ScoreToAdd;
                Drawing.DrawMenu(ScoreToAdd, Logic.WinningLinesCount);
                Statistics.CurrentStats.Score += ScoreToAdd;
                Thread.Sleep(Settings.SpinSpeed);
                Console.Clear();
            }
            if (Settings.IsAutoplay)
            {
                Console.WriteLine($"Deposited {Settings.DefaultScore} automatically");
                Thread.Sleep(Settings.SpinSpeed * 10);
            }
            else
            {
                Console.WriteLine($"You lost! Deposit some more money?\nAny button to deposit {Settings.DefaultScore}, N to quit.");
                if (Console.ReadKey().Key == ConsoleKey.N)
                {
                    Environment.Exit(0);
                }
            }
            Statistics.CurrentStats.LostCount++;
            Statistics.CurrentStats.Score = Settings.DefaultScore;
            Console.Clear();
        }
    }
}
