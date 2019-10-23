using System;

namespace WotStat
{
    internal class Computer
    {
        public static double GetWinRatio(long battleCount, long winCount)
        {
            return Math.Round(((double)winCount / battleCount * Constants.MaxWinPercent), 2);
        }

        public static long GetBattleCountToDesiredRatio(long battleCount, long winCount, double desiredRatio, double sessionRatio)
        {
            return (long)Math.Ceiling((desiredRatio / Constants.MaxWinPercent * battleCount - winCount) / (sessionRatio / Constants.MaxWinPercent - desiredRatio / Constants.MaxWinPercent));
        }
    }
}
