using Slots.Data;

namespace Slots
{
    public static class ConsoleUtils
    {
        public const int TextIndent = 1;

        public static void PrintLine(string text)
        {
            Thread.Sleep(Settings.TextSpeed);
            Console.CursorLeft += TextIndent;
            Console.WriteLine(text);
        }

        public static void Print(string text)
        {
            Thread.Sleep(Settings.TextSpeed);
            Console.CursorLeft += TextIndent;
            Console.Write(text);
        }

        public static int GetIntFromInput()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Wrong Format");
                }
            }
        }
    }
}
