using Slots.Data;
using System.Drawing;

namespace Slots
{
    public class Logic
    {

        public static List<Point> WinCells = new List<Point>();
        public static List<WinData> CurrentWinData;

        public static Symbol[,] FillBoard(int Width, int Height)
        {
            Symbol[,] board = new Symbol[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    board[i, j] = GetRandomSymbol();
                }
            }
            return board;
        }

        public static double CheckScore(Symbol[,] board)
        {
            CurrentWinData = new List<WinData>();
            double currentScore = 0;
            double resultScore = 0;

            foreach (List<int> line in SlotInfo.Lines)
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

        private static double CheckLine(List<int> lineRows, Symbol[,] board)
        {
            List<Point> winLine = new List<Point>();
            List<Point> wildWinLine = new List<Point>();
            Symbol currentSymbol = board[0, lineRows.First()];
            winLine.Add(new Point(0, lineRows.First()));
            for (int i = 1; i < SlotInfo.Width; i++)
            {
                Symbol followingSymbol = board[i, lineRows[i]];
                if (currentSymbol == SlotInfo.WildSymbol)
                {
                    currentSymbol = followingSymbol;
                }
                else if (currentSymbol == followingSymbol || followingSymbol == SlotInfo.WildSymbol)
                {
                    currentSymbol = followingSymbol == SlotInfo.WildSymbol ? currentSymbol : followingSymbol;
                }
                else
                {
                    break;
                }

                winLine.Add(new Point(i, lineRows[i]));

                if (currentSymbol == SlotInfo.WildSymbol && (winLine.Count == 3 || winLine.Count == 4))
                {
                    wildWinLine = new List<Point>(winLine);
                }
            }
            if (winLine.Count >= 3)
            {
                double result = HandleWinLine(winLine, currentSymbol);

                if (wildWinLine.Any())
                {
                    result += HandleWinLine(wildWinLine, SlotInfo.WildSymbol);
                }

                return result;
            }
            return 0;
        }

        private static double HandleWinLine(List<Point> winLine, Symbol symbol)
        {
            WinCells.AddRange(winLine);
            WinData currentLineWinData = new WinData(symbol, winLine.Count);
            if (CurrentWinData.Any(x => x.Symbol == currentLineWinData.Symbol && x.SymbolCount == currentLineWinData.SymbolCount))
            {
                int index = CurrentWinData.FindIndex(x => x.Symbol == currentLineWinData.Symbol && x.SymbolCount == currentLineWinData.SymbolCount);
                CurrentWinData[index].LineCount++;
            }
            else
            {
                CurrentWinData.Add(currentLineWinData);
            }
            return CountPayment(winLine.Count, symbol.Value);
        }

        public static double CountPayment(int count, double symbolValue)
        {
            double result = symbolValue * Settings.Bet;
            switch (count)
            {
                case 4:
                    result *= 2;
                    break;
                case 5:
                    result *= 5;
                    break;
            }
            return result;
        }

        private static Symbol GetRandomSymbol()
        {
            Random random = new Random();
            int randomNumber = random.Next(Symbol.TotalWeight);
            Symbol resultSymbol = null;
            foreach (Symbol symbol in SlotInfo.Symbols)
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
