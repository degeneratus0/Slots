using Slots.Data;

namespace Slots
{
    public static class InfoScreen
    {
        public static void DrawInfoScreen()
        {
            Console.Clear();
            Console.CursorVisible = false;
            ConsoleUtils.PrintLine($"Press any key to leave");

            for (int i = 0; i < SlotInfo.Lines.Count; i++)
            {
                DrawSlotLine(SlotInfo.Lines[i], Console.CursorTop);
                if (i % 5 == 4)
                {
                    Console.CursorTop += 5;
                    Console.CursorLeft = 0;
                    Thread.Sleep(Settings.SpinSpeed / 5);
                }
            }
            Console.CursorLeft = 0;
            for (int i = 0; i < SlotInfo.Symbols.Count; i++)
            {
                DrawSymbolInfo(SlotInfo.Symbols[i]);
            }

            Console.CursorVisible = true;
            Console.ReadKey(true);
            Console.Clear();
        }

        private static void DrawSlotLine(List<int> line, int height)
        {
            Drawing.DrawSlot();
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.CursorTop -= 5;
            int currentCursorLeft = Console.CursorLeft;
            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(currentCursorLeft + i * 4 + 1 + ConsoleUtils.TextIndent, height + line[i] + 1);
                Console.Write($"   ");
            }
            Console.CursorTop = height;
            Console.ResetColor();
        }

        private static void DrawSymbolInfo(Symbol symbol)
        {
            Console.ForegroundColor = symbol.Color;
            Console.BackgroundColor = symbol.BackgroundColor;
            ConsoleUtils.Print(symbol.Character.ToString());
            Console.ResetColor();
            ConsoleUtils.PrintLine($"- 3x: {Logic.CountPayment(3, symbol.Value):f}; " +
                $"4x: {Logic.CountPayment(4, symbol.Value):f}; " +
                $"5x: {Logic.CountPayment(5, symbol.Value):f}.");
        }
    }
}
