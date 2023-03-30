namespace Slots.Data
{
    public static class Settings
    {
        public const int BigWinX = 20;
        public const double DefaultScore = 1000;

        public static bool IsAutoplay = false;
        public static int AutoplaySpins = 0;

        private static int spinSpeed = 50;
        public static int SpinSpeed
        {
            get
            {
                return spinSpeed;
            }
            set
            {
                spinSpeed = value;
                TextSpeed = spinSpeed / 10;
            }
        }
        public static int TextSpeed = spinSpeed / 10;

        public static int Bet = 10;
    }
}
