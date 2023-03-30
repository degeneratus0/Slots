using Slots.Data;

namespace Slots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnExit);
            Console.Clear();
            ConsoleUtils.PrintLine("Press any key to start spinning!");
            Console.ReadKey(true);
            Console.Clear();
            while (true)
            {
                Game.Start();
            }
        }

        static void OnExit(object? sender, EventArgs e)
        {
            Statistics.SaveStat();
        }
    }
}