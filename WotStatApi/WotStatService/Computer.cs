using System;

namespace WotStat
{
    internal class Computer
    {
        public static double GetWinRatio(long battleCount, long winCount)
        {
            return battleCount != 0 ? Math.Round(((double)winCount / battleCount * Constants.MaxWinPercent), 2) : 0;
        }

        public static long GetBattleCountToDesiredRatio(long battleCount, long winCount, double desiredRatio, double sessionRatio)
        {
            return battleCount != 0 ? (long)Math.Ceiling((desiredRatio / Constants.MaxWinPercent * battleCount - winCount) / (sessionRatio / Constants.MaxWinPercent - desiredRatio / Constants.MaxWinPercent)) : 0;
        }
    }
}
