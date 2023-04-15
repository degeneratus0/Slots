namespace Slots.Data
{
    public class SlotInfo
    {
        public const int Width = 5;
        public const int Height = 3;

        public static readonly Symbol WildSymbol = new Symbol('W', 200, 1, bgColor: ConsoleColor.Green);
        public static readonly List<Symbol> Symbols = new List<Symbol>()
        {
            new Symbol('J', 0.2, 20),
            new Symbol('Q', 0.3, 18, ConsoleColor.Yellow),
            new Symbol('K', 0.5, 15, ConsoleColor.Blue),
            new Symbol('A', 1, 12, ConsoleColor.DarkRed),
            new Symbol('♣', 2.5, 10, ConsoleColor.Green),
            new Symbol('♦', 5, 7, ConsoleColor.DarkYellow),
            new Symbol('♥', 15, 5, ConsoleColor.DarkRed),
            new Symbol('♠', 30, 3, ConsoleColor.Blue),
            new Symbol('☺', 100, 2, bgColor: ConsoleColor.DarkYellow),
            WildSymbol
        };

        public static readonly List<List<int>> Lines = new List<List<int>>
        {
            new List<int> { 0, 0, 0, 0, 0 },
            new List<int> { 1, 1, 1, 1, 1 },
            new List<int> { 2, 2, 2, 2, 2 },
            new List<int> { 0, 0, 1, 2, 2 },
            new List<int> { 2, 2, 1, 0, 0 },
            new List<int> { 0, 1, 1, 1, 2 },
            new List<int> { 2, 1, 1, 1, 0 },
            new List<int> { 0, 1, 1, 1, 0 },
            new List<int> { 2, 1, 1, 1, 2 },
            new List<int> { 1, 0, 0, 0, 1 },
            new List<int> { 1, 2, 2, 2, 1 },
            new List<int> { 0, 2, 2, 2, 0 },
            new List<int> { 2, 0, 0, 0, 2 },
            new List<int> { 0, 1, 0, 1, 0 },
            new List<int> { 2, 1, 2, 1, 2 },
            new List<int> { 1, 1, 0, 1, 1 },
            new List<int> { 1, 1, 2, 1, 1 },
            new List<int> { 0, 0, 1, 0, 0 },
            new List<int> { 2, 2, 1, 2, 2 },
            new List<int> { 2, 2, 0, 2, 2 },
            new List<int> { 0, 0, 2, 0, 0 },
            new List<int> { 1, 0, 1, 2, 1 },
            new List<int> { 1, 2, 1, 0, 1 },
            new List<int> { 0, 1, 2, 1, 0 },
            new List<int> { 2, 1, 0, 1, 2 }
        };
    }
}
