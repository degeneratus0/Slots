using Slots.Data;

namespace Slots
{
    public static class ConsoleUtils
    {
        public static int TextSpeed = Settings.SpinSpeed / 3;

        public static void PrintLine(string text)
        {
            Thread.Sleep(TextSpeed);
            Console.WriteLine(text);
        }
        public static void Print(string text)
        {
            Thread.Sleep(TextSpeed);
            Console.Write(text);
        }
    }
}
