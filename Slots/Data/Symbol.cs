namespace Slots.Data
{
    [Serializable()]
    public class Symbol
    {
        public char Character;
        public double Value;
        public int Weight;
        public ConsoleColor Color;
        public ConsoleColor BackgroundColor;

        public static int TotalWeight = 0;

        public Symbol(char character, double value, int weight, ConsoleColor color = ConsoleColor.White, ConsoleColor bgColor = ConsoleColor.Black)
        {
            Character = character;
            Value = value;
            Weight = weight;
            Color = color;
            BackgroundColor = bgColor;
            TotalWeight += weight;
        }
    }
}
