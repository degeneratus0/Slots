using System.ComponentModel;

namespace Slots.Data
{
    public static class Settings
    {
        public const int n = 5;
        public const int m = 3;
        public const int BigWinX = 20;
        public const double DefaultScore = 1000;

        public static bool IsAutoplay = false;
        public static int AutoplaySpins = 0;

        private static int spinSpeed = 10;
        public static int SpinSpeed
        {
            get
            {
                return spinSpeed;
            }
            set
            {
                spinSpeed = value;
                ConsoleUtils.TextSpeed = spinSpeed / 3;
            }
        }
        public static int Bet = 10;
    }
}
