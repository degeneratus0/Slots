using System.Runtime.Serialization.Formatters.Binary;

namespace Slots.Data
{
    [Serializable()]
    public class Statistics
    {
        const string FileName = @"./Statistics.bin";
        public static Statistics CurrentStats = LoadStat();

        public double Score = 1000;

        public int SpinCount = 0;
        public int BigWinCount = 0;
        public double LastWin = 0;
        public List<WinData> LastWinSymbols = new List<WinData>();
        public double LastBigWin = 0;
        public List<WinData> LastBigWinSymbols = new List<WinData>();
        public double MaxBigWin = 0;
        public List<WinData> MaxBigWinSymbols = new List<WinData>();
        public double TotalWin = 0;
        public double TotalLoss = 0;
        public int LostCount = 0;

        public static Statistics LoadStat()
        {
            Statistics statistics = new Statistics();
            if (File.Exists(FileName))
            {
                Stream openFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                statistics = (Statistics)deserializer.Deserialize(openFileStream);
                openFileStream.Close();
            }
            return statistics;
        }

        public static void SaveStat()
        {
            Stream saveFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(saveFileStream, CurrentStats);
            saveFileStream.Close();
        }

        public static void EraseStat()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            CurrentStats = new Statistics();
        }

        public static void PrintStat(Statistics statistics)
        {
            Console.WriteLine();
            ConsoleUtils.PrintLine("Statistics:");
            ConsoleUtils.PrintLine($"Last win: {statistics.LastWin}");
            PrintWinData("Last win symbols: ", statistics.LastWinSymbols);
            ConsoleUtils.PrintLine($"Total number of spins: {statistics.SpinCount}");
            ConsoleUtils.PrintLine($"Total number of big wins: {statistics.BigWinCount}");
            ConsoleUtils.PrintLine($"Last big win: {statistics.LastBigWin}");
            PrintWinData("Last big win symbols: ", statistics.LastBigWinSymbols);
            ConsoleUtils.PrintLine($"Max big win: {statistics.MaxBigWin}");
            PrintWinData("Max big win symbols: ", statistics.MaxBigWinSymbols);
            ConsoleUtils.PrintLine($"Total win: {statistics.TotalWin:f}");
            ConsoleUtils.PrintLine($"Total loss: {statistics.TotalLoss}");
            ConsoleUtils.PrintLine($"Return to player: {statistics.TotalWin / statistics.TotalLoss * 100:f}%");
            ConsoleUtils.PrintLine($"Lose count: {statistics.LostCount}");
            Console.WriteLine();
        }

        private static void PrintWinData(string text, List<WinData> datas)
        {
            if (!datas.Any())
            {
                return;
            }
            ConsoleUtils.Print(text);
            for (int i = 0; i < datas.Count; i++)
            {
                PrintSymbolLine(datas[i].Symbol, datas[i].SymbolCount);
                if (datas[i].LineCount > 1)
                {
                    Console.Write($"x{datas[i].LineCount} ");
                }
                if (datas.Count > 1 && datas.Count != i + 1)
                {
                    Console.Write("| ");
                }
            }
            Console.WriteLine();
        }

        private static void PrintSymbolLine(Symbol symbol, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.ForegroundColor = symbol.Color;
                Console.BackgroundColor = symbol.BackgroundColor;
                Console.Write($"{symbol.Character}");
                Console.ResetColor();
                Console.Write($" ");
            }
        }
    }
}
