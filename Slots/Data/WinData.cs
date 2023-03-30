namespace Slots.Data
{
    [Serializable()]
    public class WinData
    {
        public Symbol Symbol;
        public int SymbolCount = 0;
        public int LineCount = 1;

        public WinData(Symbol symbol, int symbolCount)
        {
            Symbol = symbol;
            SymbolCount = symbolCount;
        }
    }
}
