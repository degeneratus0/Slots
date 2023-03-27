namespace Slots.Data
{
    public class Common
    {
        public static readonly Symbol WildSymbol = new Symbol('W', 30, 1, bgColor: ConsoleColor.Green);
        public static readonly List<Symbol> Symbols = new List<Symbol>()
        {
            new Symbol('J', 0.05, 20),
            new Symbol('Q', 0.15, 18, ConsoleColor.Yellow),
            new Symbol('K', 0.3, 15, ConsoleColor.Blue),
            new Symbol('A', 0.5, 12, ConsoleColor.DarkRed),
            new Symbol('♣', 0.75, 10, ConsoleColor.Green),
            new Symbol('♦', 1, 7, ConsoleColor.DarkYellow),
            new Symbol('♥', 2, 5, ConsoleColor.DarkRed),
            new Symbol('♠', 10, 3, ConsoleColor.Blue),
            new Symbol('☺', 20, 2, bgColor: ConsoleColor.DarkYellow),
            WildSymbol
        };
        public static readonly int[][] CustomLines = new int[][]
        {
            new int[] { 0, 0, 0, 0, 0 },
            new int[] { 1, 1, 1, 1, 1 },
            new int[] { 2, 2, 2, 2, 2 },
            new int[] { 0, 0, 1, 2, 2 },
            new int[] { 2, 2, 1, 0, 0 },
            new int[] { 0, 1, 1, 1, 0 },
            new int[] { 2, 1, 1, 1, 2 },
            new int[] { 0, 1, 0, 1, 0 },
            new int[] { 2, 1, 2, 1, 2 },
            new int[] { 1, 0, 0, 0, 1 },
            new int[] { 1, 2, 2, 2, 1 },
            new int[] { 1, 1, 0, 1, 1 },
            new int[] { 1, 1, 2, 1, 1 },
            new int[] { 1, 0, 1, 2, 1 },
            new int[] { 1, 2, 1, 0, 1 },
            new int[] { 0, 2, 0, 2, 0 },
            new int[] { 2, 0, 2, 0, 2 },
            new int[] { 0, 1, 2, 1, 0 },
            new int[] { 2, 1, 0, 1, 2 },
        };
    }
}
