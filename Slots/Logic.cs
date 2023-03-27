using Slots.Data;

namespace Slots
{
    public class Logic
    {

        public static List<(int, int)> WinCells = new List<(int, int)>();
        public static int WinningLinesCount = 0;
        public static List<WinData> CurrentWinData;

        public static void AddScore(double score)
        {
            Statistics.CurrentStats.Score += score;
        }

        public static double CheckScore(Symbol[,] board)
        {
            CurrentWinData = new List<WinData>();
            WinningLinesCount = 0;
            double currentScore = 0;
            double resultScore = 0;

            foreach (int[] line in Common.CustomLines)
            {
                currentScore += CheckLine(line, board);
            }

            if (currentScore > 0)
            {
                currentScore = Math.Round(currentScore, 2);
                resultScore = currentScore;
                CurrentWinData = CurrentWinData.OrderByDescending(x => x.Symbol.Value * x.SymbolCount).ToList();
                Statistics.CurrentStats.LastWin = resultScore;
                Statistics.CurrentStats.LastWinSymbols = CurrentWinData;
                if (resultScore >= Settings.Bet * Settings.BigWinX)
                {
                    Statistics.CurrentStats.LastBigWinSymbols = CurrentWinData;
                    Statistics.CurrentStats.LastBigWin = resultScore;
                    if (Statistics.CurrentStats.MaxBigWin < resultScore)
                    {
                        Statistics.CurrentStats.MaxBigWinSymbols = CurrentWinData;
                        Statistics.CurrentStats.MaxBigWin = resultScore;
                    }
                    Statistics.CurrentStats.BigWinCount++;
                }
            }
            return resultScore;
        }

        public static Symbol[,] FillBoard(int n, int m)
        {
            Symbol[,] board = new Symbol[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    board[i, j] = GetRandomSymbol();
                }
            }
            return board;
        }

        private static double CheckLine(int[] rows, Symbol[,] board)
        {
            List<(int, int)> winLine = new List<(int, int)>();
            int count = 0;
            Symbol currentSymbol = board[0, rows[0]];
            winLine.Add((0, rows[0]));
            for (int i = 1; i < Settings.n; i++)
            {
                Symbol boardSymbol = board[i, rows[i]];
                if (currentSymbol == Common.WildSymbol)
                {
                    count++;
                    currentSymbol = boardSymbol;
                    winLine.Add((i, rows[i]));
                    continue;
                }
                if (currentSymbol == boardSymbol || boardSymbol == Common.WildSymbol)
                {
                    count++;
                }
                else
                {
                    break;
                }
                winLine.Add((i, rows[i]));
                currentSymbol = boardSymbol == Common.WildSymbol ? currentSymbol : boardSymbol;
            }
            if (count > 1)
            {
                WinCells.AddRange(winLine);
                WinningLinesCount++;
                WinData currentLine = new WinData(currentSymbol, count);
                if (CurrentWinData.Any(x => x.Symbol.Character == currentLine.Symbol.Character && x.SymbolCount == currentLine.SymbolCount))
                {
                    int index = CurrentWinData.FindIndex(x => x.Symbol.Character == currentLine.Symbol.Character && x.SymbolCount == currentLine.SymbolCount);
                    CurrentWinData[index].LineCount++;
                }
                else
                {
                    CurrentWinData.Add(currentLine);
                }
                return CountPayment(count, currentSymbol);
            }
            return 0;
        }

        private static double CountPayment(int count, Symbol symbol)
        {
            return symbol.Value * Math.Pow(count, 2) * Settings.Bet * count / 2;
        }

        private static Symbol GetRandomSymbol()
        {
            Random random = new Random();
            int randomNumber = random.Next(Symbol.TotalWeight);
            Symbol resultSymbol = null;
            foreach (Symbol symbol in Common.Symbols)
            {
                if (randomNumber < symbol.Weight)
                {
                    resultSymbol = symbol;
                    break;
                }
                randomNumber -= symbol.Weight;
            }
            return resultSymbol;
        }
    }
}
